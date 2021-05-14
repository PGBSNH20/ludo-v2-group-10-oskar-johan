﻿using Ludo_API.Models;
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
            return !(squareToCheck is null || squareToCheck.Tenant?.Player == player);
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

                bool canInsertToken = CanInsertTokenAt(Squares[currentSquareIndex], player);

                if (!canInsertToken)
                {
                    return false;
                }

                // If it's the last Square, return true since the Piece can move to the current Square.
                if (i == diceRoll)
                {
                    return true; // todo: combine this with line 47?
                }

                // The Piece can be moved to the current square, continue to next square.
                continue;
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
            if (player == null || diceNumber is < 1 or > 6)
            {
                throw new Exception("Player is null or diceNumber outside the valid range.");
            }

            int piecesOnBoardCount = 0;
            List<MoveAction> moveActions = new();

            var playerOwnedSquares = Squares.Where(s =>
            {
                // todo: fix the "OccupiedBy != null when s.Tenant?.PieceCount == 0" bug and remove " && s.Tenant?.PieceCount > 0"
                //bool hasTokenOnSquare = s..Tenant?.Player == player && s.Tenant?.PieceCount > 0 && s.ID != player.Track[^1];
                //bool hasTokenOnSquare = s.Tenant.Player == player && s.Tenant?.PieceCount > 0 && s.ID != player.Track[^1];
                piecesOnBoardCount += s.Tenant?.PieceCount ?? 0;
                return s.Tenant?.Player == player && s.ID != player.Track[^1];
            });

            // "Move piece" actions:
            #region "Insert New Piece(s)" actions:
            foreach (Square square in playerOwnedSquares)
            {
                bool validMove = CanMoveToSquare(player, square, diceNumber, out Square destinationSquare);

                if (square.Tenant.PieceCount == 1)
                {
                    moveActions.Add(new()
                    {
                        PlayerId = player.ID,
                        ValidMove = validMove,
                        DiceRoll = diceNumber,
                        OptionText = $"Move your piece from {square.ID} to {destinationSquare.ID}",
                        StartSquare = new SquareTenant(square.ID, null, 0),
                        DestinationSquare = new SquareTenant(destinationSquare.ID, player, 1),
                    });
                }
                else if (square.Tenant.PieceCount == 2)
                {
                    moveActions.Add(new()
                    {
                        PlayerId = player.ID,
                        ValidMove = validMove,
                        DiceRoll = diceNumber,
                        OptionText = $"Move one of your pieces from {square.ID} to {destinationSquare.ID}",
                        StartSquare = new SquareTenant(square.ID, player, 1),
                        DestinationSquare = new SquareTenant(destinationSquare.ID, player, 2),
                    });
                }
                else
                {
                    throw new Exception($"Unexpected square.Tenant?.PieceCount: {square.Tenant?.PieceCount}");
                }
            }
            #endregion

            #region "Insert New Piece(s)" actions:
            if (piecesOnBoardCount <= 2 && CanInsertTokenAt(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 6)
            {
                moveActions.Add(new()
                {
                    PlayerId = player.ID,
                    ValidMove = true,
                    DiceRoll = diceNumber,
                    OptionText = $"Insert two new <b>pieces<b> to <b>square {player.StartPosition}</b>.",
                    DestinationSquare = new SquareTenant(player.StartPosition, player, 2)
                });
            }

            if (piecesOnBoardCount < 4)
            {
                if (CanInsertTokenAt(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 1)
                {
                    moveActions.Add(new()
                    {
                        PlayerId = player.ID,
                        ValidMove = true,
                        DiceRoll = diceNumber,
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
                        DiceRoll = diceNumber,
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
