using Ludo_API.Data;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class Game
    {
        private readonly ILudoData2 _ludoData2;

        public Gameboard Gameboard { get; set; }
        public ICollection<Square> Squares { get; set; }

        public Game()
        {
            //_ludoData2 = ludoData2;
        }

        public bool CanInsertTokenAt(Square squareToCheck, Player player)
        {
            return !(squareToCheck is null || squareToCheck.Tenant?.Player == player);
        }

        static public class MoveMessagesClass
        {
            public const string NoPossibleMoves = "No possible moves";
            public const string MoveSuccessful = "Move successful";
            public const string CantPassYourOwn = "You can't pass or stop on a square you occupy";
            public const string KnockOutOpponent = "You've knocked your opponent's piece(s) out.";
            public const string PieceEnteredGoal = "You've moved a piece to the goal square.";
        }

        public (bool valid, string message) CanMoveToSquare(Player player, Square startSquare, int diceRoll, out Square endSquare)
        {
            var playerTrack = LudoData.Instance.ColorTracks[player.Color];
            Square initialSquare = Squares.ElementAt(startSquare.ID);
            int startIndex = playerTrack.TrackIndices.ToList().FindIndex(x => x == startSquare.ID);
            int currentPlayerTrackIndex = startIndex;
            bool moveBackwards = false;

            for (int i = 1; i <= diceRoll; i++)
            {
                // todo: fix (square vs playerTrack index) naming
                currentPlayerTrackIndex += moveBackwards ? -1 : 1;
                int? currentSquareIndex;
                try
                {
                    currentSquareIndex = playerTrack.TrackIndices[currentPlayerTrackIndex];
                    endSquare = Squares.ElementAt(currentSquareIndex.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("currentPlayerTrackIndex is likely out of bounds");
                }

                // If the Piece is on the "goal"-Square this iteration.
                if (currentSquareIndex == playerTrack.GoalIndex)
                {
                    // If the Piece is in the "goal"-Square and has finished.
                    if (i == diceRoll)
                    {
                        return (true, MoveMessagesClass.PieceEnteredGoal);
                    }

                    // The Piece has to continue moving, it should move backwards the following iterations.
                    moveBackwards = true;
                }

                // This checks whether the player has a token on this square, false is expected if they do since they can't move to or past this square.
                bool canInsertToken = CanInsertTokenAt(Squares.ElementAt(currentSquareIndex.Value), player);

                if (!canInsertToken)
                {
                    return (false, MoveMessagesClass.CantPassYourOwn);
                }

                // If it's the last Square, return true since the Piece can move to the current Square.
                if (i == diceRoll)
                {
                    return (true, MoveMessagesClass.MoveSuccessful); // todo: combine this with line 47?
                }

                // The Piece can be moved to the current square, continue to next square.
                continue;
            }

            // Unexpected?
            //return true; // note: is true correct?
            throw new Exception("Unexpected exception in Game.CanMoveToSquare()"); // is it correct to throw? the loop should handle all cases where we want to return a bool.
        }

        public List<MoveAction> GetPossibleMoves(Gameboard gameboard, Player player, int diceNumber)
        {
            Gameboard = gameboard;
            Squares = gameboard.Squares;
            var playerTrack = LudoData.Instance.ColorTracks[player.Color];

            if (player == null || diceNumber is < 1 or > 6)
            {
                throw new Exception("Player is null or diceNumber outside the valid range.");
            }

            int piecesOnBoardCount = 0;
            List<MoveAction> moveActions = new();

            var playerOwnedSquares = Squares.Where(s =>
            {
                if (s.Tenant?.Player == player)
                {
                    piecesOnBoardCount += s.Tenant?.PieceCount ?? 0;
                    return s.ID != playerTrack.GoalIndex;
                }

                return false;
            });

            // "Move piece" actions:
            #region "Insert New Piece(s)" actions:
            foreach (Square square in playerOwnedSquares)
            {
                var (validMove, message) = CanMoveToSquare(player, square, diceNumber, out Square destinationSquare);

                if (square.Tenant.PieceCount == 1)
                {
                    moveActions.Add(new()
                    {
                        GameId = Gameboard.ID,
                        //PlayerId = player.ID,
                        Player = player,
                        ValidMove = validMove,
                        DiceRoll = diceNumber,
                        OptionText = $"Move your piece from {square.ID} to {destinationSquare.ID}",
                        Message = message,
                        StartSquare = new SquareTenant(square.ID, null, 0),
                        DestinationSquare = new SquareTenant(destinationSquare.ID, player, 1),
                    });
                }
                else if (square.Tenant.PieceCount == 2)
                {
                    moveActions.Add(new()
                    {
                        GameId = Gameboard.ID,
                        //PlayerId = player.ID,
                        Player = player,
                        ValidMove = validMove,
                        DiceRoll = diceNumber,
                        OptionText = $"Move one of your pieces from {square.ID} to {destinationSquare.ID}",
                        Message = message,
                        StartSquare = new SquareTenant(square.ID, player, 1),
                        DestinationSquare = new SquareTenant(destinationSquare.ID, player, 1),
                    });
                }
                else
                {
                    throw new Exception($"Unexpected square.Tenant?.PieceCount: {square.Tenant?.PieceCount}");
                }
            }
            #endregion

            #region "Insert New Piece(s)" actions:
            if (piecesOnBoardCount <= 2 && CanInsertTokenAt(Gameboard.GetSquare(playerTrack.StartIndex), player) && diceNumber == 6)
            {
                var destinationSquare = new SquareTenant(playerTrack.StartIndex, player, 2);

                moveActions.Add(new()
                {
                    GameId = Gameboard.ID,
                    Player = player,
                    ValidMove = true,
                    DiceRoll = diceNumber,
                    OptionText = $"Insert two new pieces to square {playerTrack.StartIndex}.",
                    Message = Gameboard.GetSquare(destinationSquare.SquareIndex).Tenant != null ? MoveMessagesClass.KnockOutOpponent : MoveMessagesClass.MoveSuccessful,
                    DestinationSquare = destinationSquare,
                });
            }

            if (piecesOnBoardCount < 4)
            {
                if (CanInsertTokenAt(Gameboard.GetSquare(playerTrack.StartIndex), player) && diceNumber == 1)
                {
                    var destinationSquare = new SquareTenant(playerTrack.StartIndex, player, 1);

                    moveActions.Add(new()
                    {
                        GameId = Gameboard.ID,
                        Player = player,
                        ValidMove = true,
                        DiceRoll = diceNumber,
                        OptionText = $"Insert a new piece to square {playerTrack.StartIndex}.",
                        Message = Gameboard.GetSquare(destinationSquare.SquareIndex).Tenant != null ? MoveMessagesClass.KnockOutOpponent : MoveMessagesClass.MoveSuccessful,
                        DestinationSquare = destinationSquare,
                    });
                }
                else if (CanInsertTokenAt(Gameboard.GetSquare(playerTrack.StartIndex + 5), player) && diceNumber == 6)
                {
                    var destinationSquare = new SquareTenant(playerTrack.StartIndex + 5, player, 1);

                    moveActions.Add(new()
                    {
                        GameId = Gameboard.ID,
                        Player = player,
                        ValidMove = true,
                        DiceRoll = diceNumber,
                        OptionText = $"Insert a new piece to square {playerTrack.StartIndex + 5}.",
                        Message = Gameboard.GetSquare(destinationSquare.SquareIndex).Tenant != null ? MoveMessagesClass.KnockOutOpponent : MoveMessagesClass.MoveSuccessful,
                        DestinationSquare = destinationSquare,
                    });
                }
            }
            #endregion

            if (moveActions.Count == 0)
            {
                moveActions.Add(new()
                {
                    GameId = Gameboard.ID,
                    //PlayerId = player.ID,
                    Player = player,
                    ValidMove = true,
                    DiceRoll = diceNumber,
                    OptionText = "You cannot make any possible moves.",
                    Message = MoveMessagesClass.NoPossibleMoves,
                    StartSquare = null,
                    DestinationSquare = null,
                });
            }

            return moveActions;
        }
    }
}
