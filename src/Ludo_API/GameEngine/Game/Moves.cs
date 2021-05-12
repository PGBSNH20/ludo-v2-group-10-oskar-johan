using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class Moves
    {
        private readonly IGameEngine _gameEngine;
        private readonly IGamesRepository _gameRepository;

        public Moves(IGamesRepository gameRepository, IGameEngine gameEngine)
        {
            _gameRepository = gameRepository;
            _gameEngine = gameEngine;
        }

        public void GameMove(Gameboard gameboard, List<Square> squares, int diceNumber, Models.Player player)
        {
            // Check if a player can insert a new piece on the board (into their start position (1st square) or "their 6th square"
            bool isMoveToSixPossible = CheckMoveOut(squares, player, player.StartPosition + 5);
            bool isMoveToFirstPossible = CheckMoveOut(squares, player, player.StartPosition);

            // A count of the player's piece on the board (including pieces in the "goal")
            int piecesOnBoardCount = 0;
            // A count of the player's piece on their start position/square
            int piecesOnStartCount = 0;
            List<int> piecesPositions = new();

            // get the id (index) of all of the player's pieces (excluding pieces in the "goal"),
            // and calculate how many pieces the player has on the their start position/square, and how many peieces they have on the board (including in the "goal")
            //foreach (var square in squares)
            //{
            //    if (square.OccupiedBy == player && square.PieceCount > 0)
            //    {
            //        piecesOnBoardCount += square.PieceCount ?? 0;

            //        if (square.ID == player.StartPosition)
            //        {
            //            piecesOnStartCount = square.PieceCount ?? 0;
            //        }

            //        if (square.ID != player.Track[^1])
            //        {
            //            piecesPositions.Add(square.ID);
            //        }
            //    }
            //}

            // The list of menu-options and their actions
            List<string> options = new();
            List<Func<bool>> optionActions = new();

            #region Populate the menu, and the list of Actions for the various options
             //< --Add possible actions to list
            foreach (int position in piecesPositions)
            {
                if (position == player.StartPosition && piecesOnStartCount == 2)
                {
                    //options.Add($"Flytta en av dina pjäser på ruta {position}");
                    //optionActions.Add(new Func<bool>(() =>
                    //{
                    //    return MovePiece(gameboard, squares, player, position, diceNumber);
                    //}));
                }
                else
                {
                    //options.Add($"Flytta pjäs på ruta {position}");
                    //optionActions.Add(new Func<bool>(() =>
                    //{
                    //    return MovePiece(gameboard, squares, player, position, diceNumber);
                    //}));
                }
            }

            // Add the menu-option and Action for adding 2 new pieces onto the board at the player's start position/square
            if (isMoveToFirstPossible && diceNumber == 6 && piecesOnBoardCount <= 2)
            {
                options.Add($"Ta ut två spelpjäser till första rutan");
                optionActions.Add(new Func<bool>(() =>
                {
                    return AddPieces(squares, player, player.StartPosition, 2);
                }));
            }

            // Add the menu-option(s) and Action(s) for adding 1 new pieces onto the board at the player's 1st and/or 6th position/square.
            if (piecesOnBoardCount < 4)
            {
                if (isMoveToFirstPossible && diceNumber == 1)
                {
                    options.Add($"Ta ut en spelpjäs till ruta {player.StartPosition}");
                    optionActions.Add(new Func<bool>(() =>
                    {
                        return AddPieces(squares, player, player.StartPosition, 1);
                    }));
                }
                else if (isMoveToSixPossible && diceNumber == 6)
                {
                    options.Add($"Ta ut en spelpjäs till ruta {player.StartPosition + 5}");
                    optionActions.Add(new Func<bool>(() =>
                    {
                        return AddPieces(squares, player, player.StartPosition + 5, 1);
                    }));
                }
            }
            // Add possible actions to list -->
            #endregion Populate the menu, and the list of Actions for the various options

            // If the player has possible moves, draw the gameboard and present the menu of options
            //if (options.Count > 0)
            //{
            //    // Show the menu until the player has selected a valid move, or they've exhausted their options
            //    while (true)
            //    {
            //        //Console.Clear();
            //        //GameEngine.ShowGameboardInline(gameboard);

            //        //Console.WriteLine($"{player.Name} slog {diceNumber}");

            //        //var choice = Menu.ShowMenu("\nVad vill du göra nu?", options.ToArray());
            //        //bool successful = optionActions[choice]();

            //        // When the player has chosen a valid move, break out of the loop
            //        //if (successful)
            //        //{
            //        //    break;
            //        //}

            //        // The move was unsuccessful, remove its menuitem and Action.
            //        //Console.WriteLine("Draget är inte möjligt. Flytta en annan pjäs.");
            //        //options.RemoveAt(choice);
            //        //optionActions.RemoveAt(choice);

            //        // If the player has tried every menu option without success
            //        if (optionActions.Count == 0)
            //        {
            //            //Console.WriteLine($"Det verkar som att du ({player.Name}) inte har några möjliga drag.");

            //            if (diceNumber != 6)
            //            {
            //                //Console.WriteLine("\nTuren går vidare till nästa spelare.");
            //            }

            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"{player.Name} slog {diceNumber}, och kan inte flytta någon pjäs.\nNästa spelares tur!");
            //}
        }

        public static bool AddPieces(List<Square> squares, Player player, int position, int count)
        {
            switch (count)
            {
                case 1:
                    // Add one piece to 6th
                    if (squares[position].OccupiedBy != null && squares[position].PieceCount > 0)
                    {
                        Console.WriteLine($"Du knuffade ut {squares[position].OccupiedBy.Name}s pjäs");

                    }

                    squares[position].OccupiedBy = player;
                    squares[position].PieceCount = count;

                    Console.WriteLine($"Du står på ruta {squares[position].ID}.");

                    break;

                case 2:
                    // Add two pieces to first
                    if (squares[position].OccupiedBy != null && squares[position].PieceCount > 0)
                    {
                        Console.WriteLine($"Du knuffade ut {squares[position].OccupiedBy.Name}s pjäs");

                    }

                    squares[position].OccupiedBy = player;
                    squares[position].PieceCount = count;

                    Console.WriteLine($"Du står på ruta {squares[position].ID}.");

                    break;
            }
            return true;
        }

        //public bool MovePiece(Gameboard gameboard, List<Square> squares, Models.Player player, int initialPosition, int diceNumber)
        //{
        //    int startIndex = player.Track.FindIndex(x => x == initialPosition);
        //    Square initialSquare = squares[initialPosition];

        //    for (int i = 1; i <= diceNumber; i++)
        //    {
        //        int currentIndex = player.Track[startIndex + i];
        //        Square currentSquare = squares[currentIndex];
        //        bool backwardsMove = false;

        //        if (currentIndex != player.GoalIndex && player == currentSquare.OccupiedBy && currentSquare.PieceCount > 0)
        //        {
        //            Console.WriteLine("Du får inte passera din egen pjäs.\n");
        //            return false;
        //        }

        //        if (i == diceNumber)
        //        {
        //            if (currentIndex == player.GoalIndex)
        //            {
        //                initialSquare.OccupiedBy = null;
        //                initialSquare.PieceCount = 0;

        //                currentSquare.OccupiedBy = player;
        //                currentSquare.PieceCount++;

        //                Console.WriteLine("Du har gått i mål med en pjäs!");

        //                if (currentSquare.PieceCount == 4)
        //                {
        //                    gameboard.LastPlayer = null;
        //                    _gameRepository.SaveTurnAsync(gameboard, player);
        //                    Console.WriteLine($"Grattis {currentSquare.OccupiedBy.Name}!! Du har vunnit spelet!\n");
        //                    _gameEngine.EndGame();
        //                }
        //                return true;
        //            }
        //            else if (player == currentSquare.OccupiedBy && currentSquare.PieceCount > 0)
        //            {
        //                Console.WriteLine("Du får inte stå där du redan har en pjäs!");
        //                return false;
        //            }
        //            else if (currentSquare.OccupiedBy != null && player.Color != currentSquare.OccupiedBy.Color && currentSquare.PieceCount > 0)
        //            {
        //                Console.WriteLine($"Du knuffade ut {currentSquare.OccupiedBy.Name}s pjäs!");

        //                initialSquare.PieceCount--;
        //                if (initialSquare.PieceCount == 0)
        //                {
        //                    initialSquare.OccupiedBy = null;
        //                }
        //                else if (initialSquare.PieceCount < 0)
        //                {
        //                    throw new Exception("Piece count can't be under 0!");
        //                }

        //                currentSquare.OccupiedBy = player;
        //                currentSquare.PieceCount = 1;

        //                return true;
        //            }

        //            initialSquare.PieceCount--;

        //            if (initialSquare.PieceCount == 0)
        //            {
        //                initialSquare.OccupiedBy = null;
        //            }
        //            else if (initialSquare.PieceCount < 0)
        //            {
        //                throw new Exception("Piece count can't be under 0!");
        //            }

        //            currentSquare.OccupiedBy = player;
        //            currentSquare.PieceCount = 1;

        //            Console.WriteLine($"Du står på ruta {currentSquare.ID}.");

        //            return true;
        //        }

        //        if (currentIndex == player.GoalIndex && i < diceNumber)
        //        {
        //            if (diceNumber - i >= 1)
        //            {
        //                // todo: remove ugly hack
        //                backwardsMove = (new Game()).MoveBackwards(squares, player, diceNumber - i, startIndex + i, initialSquare);
        //            }
        //            return backwardsMove;
        //        }
        //    }
        //    return true;
        //}

        public static bool CheckMoveOut(List<Square> squares, Player player, int position)
        {
            if (squares[position].OccupiedBy == player && squares[position].PieceCount > 0)
            {
                return false;
            }

            return true;
        }
    }
}
