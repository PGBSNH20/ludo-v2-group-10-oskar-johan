using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class Game
    {
        //private readonly IGamesRepository _gameRepository;
        public Gameboard Gameboard { get; }
        public List<Square> Squares { get; }

        public Game(IGamesRepository gameRepository, Gameboard gameboard)
        {
            //_gameRepository = gameRepository;
            Gameboard = gameboard;
            Squares = gameboard.Squares;
        }

        public bool CanInsertTokenAt(Square squareToCheck, Player player)
        {
            // note: this assumes that the OccupiedBy != null when PieceCount > 0 bug is fixed.
            return !(squareToCheck is null || squareToCheck.OccupiedBy == player);
        }

        public bool CanMoveToSquare(Player player, Square startSquare, int diceRoll, out Square endSquare)
        {
            Square initialSquare = Squares[startSquare.ID];
            int startIndex = player.Track.FindIndex(x => x == startSquare.ID);
            int currentPlayerTrackIndex = startIndex;
            bool moveBackwards = false;

            for (int i = 1; i <= diceRoll; i++)
            {
                // todo: fix (square vs playerTrack index) naming
                currentPlayerTrackIndex += moveBackwards ? -1 : 1;
                int currentSquareIndex = player.Track[currentPlayerTrackIndex];
                endSquare = Squares[currentSquareIndex];

                // If the Piece is on the "goal"-Square this iteration.
                if (currentSquareIndex == player.GoalIndex)
                {
                    // If the Piece is in the "goal"-Square and has finished.
                    if (i == diceRoll) { return true; }

                    // The Piece has to continue moving, it should move backwards the following iterations.
                    moveBackwards = true;
                }

                return CanInsertTokenAt(Squares[currentSquareIndex], player);
            }

            //return true; // note: is true correct?
            throw new Exception(""); // is it correct to throw? the loop should handle all cases where we want to return a bool.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="diceNumber"></param>
        /// <returns></returns>
        public List<MoveAction> GetPossibleMoves(Player player, int diceNumber)
        {
            int tokensInPlay = 0;
            List<MoveAction> moveActions = new();

            var playerSquares = Squares.Where(s =>
            {
                // todo: fix the "OccupiedBy != null when s.PieceCount == 0" bug and remove " && s.PieceCount > 0"
                bool hasTokenOnSquare = s.OccupiedBy == player && s.PieceCount > 0 && s.ID != player.Track[^1];

                if (hasTokenOnSquare)
                {
                    tokensInPlay += s.PieceCount.Value;
                }

                return hasTokenOnSquare;
            });

            // "Move piece" actions:
            foreach (Square square in playerSquares)
            {
                moveActions.Add(new()
                {
                    PlayerId = player.ID,
                    ValidMove = CanMoveToSquare(player, square, diceNumber, out Square destinationSquare),
                    OptionText = $"Move piece from {square.ID} to {destinationSquare.ID}",
                    StartSquare = new SquareTenant(square.ID, null, 0), // todo: if move from first square with 2 pieces this should be 'new SquareTenant(square.ID, player, 1)
                    DestinationSquare = new SquareTenant(destinationSquare.ID, player, 2)
                });
            }

            #region "Insert New Piece(s)" actions:
            if (tokensInPlay <= 2 && CanInsertTokenAt(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 6)
            {
                moveActions.Add(new()
                {
                    PlayerId = player.ID,
                    ValidMove = true,
                    OptionText = $"Insert two new <b>pieces<b> to <b>square {player.StartPosition}</b>.",
                    DestinationSquare = new SquareTenant(player.StartPosition, player, 2)
                });
            }

            if (tokensInPlay < 4)
            {
                if (CanInsertTokenAt(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 1)
                {
                    moveActions.Add(new()
                    {
                        PlayerId = player.ID,
                        ValidMove = true,
                        OptionText = $"Insert a new <b>piece<b> to <b>square {player.StartPosition}</b>.",
                        DestinationSquare = new SquareTenant(player.StartPosition, player, 1)
                    });
                }
                else if (CanInsertTokenAt(Gameboard.GetSquare(player.StartPosition + 5), player) && diceNumber == 6)
                {
                    moveActions.Add(new()
                    {
                        PlayerId = player.ID,
                        ValidMove = true,
                        OptionText = $"Insert a new <b>piece<b> to <b>square {player.StartPosition + 5}</b>.",
                        DestinationSquare = new SquareTenant(player.StartPosition + 5, player, 1)
                    });
                }
            }
            #endregion

            return moveActions;
        }
    }
}
