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
        private readonly IGamesRepository _gameRepository;
        public Gameboard Gameboard { get; }
        public List<Square> Squares { get; }

        public Game(IGamesRepository gameRepository, Gameboard gameboard)
        {
            _gameRepository = gameRepository;
            Gameboard = gameboard;
            Squares = gameboard.Squares;
        }

        #region ILudoGame
        public void GetPossibleMoves(Player player, int diceNumber)
        {
            List<Square> moveableTokens = new();
            // todo: if we can fix the occupiedBy bug we can get rid of 's.PieceCount > 0'.
            int tokensInPlay = 0;
            var playerSquares = Squares.Where(s =>
            {
                bool hasTokenOnSquare = s.OccupiedBy == player && s.PieceCount > 0 && s.ID != player.Track[^1];

                if (hasTokenOnSquare)
                {
                    tokensInPlay += s.PieceCount.Value;
                }

                return hasTokenOnSquare;
            });

            // Add the menu-option and Action for adding 2 new pieces onto the board at the player's start position/square
            //if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition), player) != PossibleMoveAction.None && diceNumber == 6 && tokensInPlay <= 2)
            if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 6 && tokensInPlay <= 2)
            {
                new MoveAction
                {
                    SquareIndex = player.StartPosition,
                    OptionText = $"Ta ut två spelpjäser till första rutan",
                    NewPlayerValue = player,
                    NewPieceCount = 2,
                };
                //options.Add($"Ta ut två spelpjäser till första rutan");
                //optionActions.Add(new Func<bool>(() =>
                //{
                //    return AddPieces(squares, player, player.StartPosition, 2);
                //}));
            }

            // Add the menu-option(s) and Action(s) for adding 1 new pieces onto the board at the player's 1st and/or 6th position/square.
            if (tokensInPlay < 4)
            {
                //if (!Squares.Any(s => s.ID == player.StartPosition && s.OccupiedBy == player && s.PieceCount > 0) && diceNumber == 1)
                //if (CanMoveToSquare(Squares[player.StartPosition], player) != PossibleMoveAction.None && diceNumber == 1)
                //if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition), player) != PossibleMoveAction.None && diceNumber == 1)
                if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition), player) && diceNumber == 1)
                {
                    new MoveAction
                    {
                        SquareIndex = player.StartPosition,
                        OptionText = $"Ta ut en spelpjäs till ruta {player.StartPosition}",
                        NewPlayerValue = player,
                        NewPieceCount = 1,
                    };
                    //options.Add();
                    //optionActions.Add(new Func<bool>(() =>
                    //{
                    //    return AddPieces(squares, player, player.StartPosition, 1);
                    //}));
                }
                //else if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition + 5), player) != PossibleMoveAction.None && diceNumber == 6)
                else if (CanMoveToSquare(Gameboard.GetSquare(player.StartPosition + 5), player) && diceNumber == 6)
                {
                    new MoveAction
                    {
                        SquareIndex = player.StartPosition + 5,
                        OptionText = $"Ta ut en spelpjäs till ruta {player.StartPosition + 5}",
                        NewPlayerValue = player,
                        NewPieceCount = 1,
                    };
                    //options.Add();
                    //optionActions.Add(new Func<bool>(() =>
                    //{
                    //    return AddPieces(squares, player, player.StartPosition + 5, 1);
                    //}));
                }
            }
            else
            {
                foreach (var square in playerSquares)
                {
                    if (CanMoveToken(player, square, diceNumber))
                    {
                        moveableTokens.Add(square);
                    }
                }
            }


            //if (player)
        }


        public static bool CheckMoveOut(List<Square> squares, Player player, int position)
        {
            if (squares[position].OccupiedBy == player && squares[position].PieceCount > 0)
            {
                return false;
            }

            return true;
        }

        public void GetPossibleMoves2(Gameboard gameboard, List<Square> squares, int diceNumber, Models.Player player)
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
            foreach (var square in squares)
            {
                if (square.OccupiedBy == player && square.PieceCount > 0)
                {
                    piecesOnBoardCount += square.PieceCount ?? 0;

                    if (square.ID == player.StartPosition)
                    {
                        piecesOnStartCount = square.PieceCount ?? 0;
                    }

                    if (square.ID != player.Track[^1])
                    {
                        piecesPositions.Add(square.ID);
                    }
                }
            }

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

        public enum PossibleMoveAction
        {
            None,
            Move,
            Knockout
        };

        //public bool CanMoveTo(List<Square> squares, Player player, int position)
        //public PossibleMoveAction CanMoveToSquare(Square squareToCheck, Player player)
        public bool CanMoveToSquare(Square squareToCheck, Player player)
        {
            // note: this assumes that the OccupiedBy != null when PieceCount > 0 bug is fixed.
            if (squareToCheck is null || squareToCheck.OccupiedBy == player)
            {
                return false;
            }

            return true;

            //if (squareToCheck == null)
            //{
            //    //return PossibleMoveAction.None;
            //    return false;
            //}

            //if (squareToCheck.PieceCount > 0)
            //{

            //    if (squareToCheck.OccupiedBy == player)
            //    {
            //        //return PossibleMoveAction.None;
            //        return false;
            //    }
            //    else if (squareToCheck.OccupiedBy != null)
            //    {
            //        //return PossibleMoveAction.Knockout;
            //        return true;
            //    }
            //}

            ////return PossibleMoveAction.Move;
            //return true;
        }

        public void AddToken()
        {
            throw new NotImplementedException();
        }

        public void MoveToken(Player player, Square startSquare, Square endSquare)
        {
            _gameRepository.MoveToken(player, startSquare, endSquare);
            //throw new NotImplementedException();
        }

        public bool IsTokenMovePossible(Player player, Square startSquare, int diceRoll)
        {
            Square initialSquare = Squares[startSquare.ID];
            int startIndex = player.Track.FindIndex(x => x == startSquare.ID);
            int currentPlayerTrackIndex = startIndex;
            bool moveBackwards = false;

            for (int i = 1; i <= diceRoll; i++)
            {
                currentPlayerTrackIndex += moveBackwards ? -1 : 1;
                int currentSquareIndex = player.Track[currentPlayerTrackIndex];
                //Square currentSquare = Squares[currentSquareIndex];

                // If it's the last move.
                if (currentSquareIndex == player.GoalIndex)
                {
                    if (i == diceRoll)
                    {
                        // goal
                        return true;
                    }

                    // move backwards
                    moveBackwards = true;
                }

                // normal move
                //bool canMove = CanMoveToSquare(currentSquare, player);
                return CanMoveToSquare(Squares[currentSquareIndex], player);
            }

            throw new Exception("");
        }

        public bool CanMoveToken(Player player, Square startSquare, int diceNumber)
        {
            int startIndex = player.Track.FindIndex(x => x == startSquare.ID);
            Square initialSquare = Squares[startSquare.ID];

            for (int i = 1; i <= diceNumber; i++)
            {
                int currentIndex = player.Track[startIndex + i];
                Square currentSquare = Squares[currentIndex];
                bool backwardsMove = false;

                if (currentIndex != player.GoalIndex && player == currentSquare.OccupiedBy && currentSquare.PieceCount > 0)
                {
                    //Console.WriteLine("Du får inte passera din egen pjäs.\n");
                    return false;
                }

                // If it's the last move.
                if (i == diceNumber)
                {
                    if (currentIndex == player.GoalIndex)
                    {
                        //initialSquare.OccupiedBy = null;
                        //initialSquare.PieceCount = 0;

                        //currentSquare.OccupiedBy = player;
                        //currentSquare.PieceCount++;

                        //Console.WriteLine("Du har gått i mål med en pjäs!");

                        // todo: move some of this to its own method.
                        if (currentSquare.PieceCount == 4)
                        {
                            Gameboard.LastPlayer = null;
                            _gameRepository.SaveTurnAsync(Gameboard, player);
                            //Console.WriteLine($"Grattis {currentSquare.OccupiedBy.Name}!! Du har vunnit spelet!\n");
                            // todo: restore next line
                            //_gameEngine.EndGame();
                        }

                        return true;
                    }
                    else if (player == currentSquare.OccupiedBy && currentSquare.PieceCount > 0)
                    {
                        //Console.WriteLine("Du får inte stå där du redan har en pjäs!");
                        return false;
                    }
                    else if (currentSquare.OccupiedBy != null && player.Color != currentSquare.OccupiedBy.Color && currentSquare.PieceCount > 0)
                    {
                        //Console.WriteLine($"Du knuffade ut {currentSquare.OccupiedBy.Name}s pjäs!");

                        initialSquare.PieceCount--;
                        if (initialSquare.PieceCount == 0)
                        {
                            //initialSquare.OccupiedBy = null;
                        }
                        else if (initialSquare.PieceCount < 0)
                        {
                            throw new Exception("Piece count can't be under 0!");
                        }

                        //currentSquare.OccupiedBy = player;
                        //currentSquare.PieceCount = 1;

                        return true;
                    }

                    //initialSquare.PieceCount--;

                    if (initialSquare.PieceCount == 0)
                    {
                        //initialSquare.OccupiedBy = null;
                    }
                    else if (initialSquare.PieceCount < 0)
                    {
                        throw new Exception("Piece count can't be under 0!");
                    }

                    //currentSquare.OccupiedBy = player;
                    //currentSquare.PieceCount = 1;

                    //Console.WriteLine($"Du står på ruta {currentSquare.ID}.");

                    return true;
                }

                if (currentIndex == player.GoalIndex && i < diceNumber)
                {
                    if (diceNumber - i >= 1)
                    {
                        backwardsMove = MoveBackwards(Gameboard.Squares, player, diceNumber - i, startIndex + i, initialSquare);
                    }

                    return backwardsMove;
                }
            }

            return true;
        }

        public bool MoveBackwards(List<Square> squares, Models.Player player, int remainingJumps, int currentIndex, Square initialSquare)
        {
            Square nextSquare = initialSquare;

            for (int i = 1; i <= remainingJumps; i++)
            {
                int nextIndex = player.Track[currentIndex - i];
                nextSquare = Gameboard.Squares[nextIndex];

                if (nextSquare != initialSquare && nextSquare.OccupiedBy == player && nextSquare.PieceCount > 0)
                {
                    //Console.WriteLine("Du får inte passera din egen pjäs!\n");
                    return false;
                }

                if (nextSquare.OccupiedBy != null && player.Color != nextSquare.OccupiedBy.Color && nextSquare.PieceCount > 0)
                {
                    //Console.WriteLine($"Du knuffade ut {nextSquare.OccupiedBy.Name}s pjäs!");

                    //initialSquare.OccupiedBy = null;
                    //initialSquare.PieceCount = 0;

                    //nextSquare.OccupiedBy = player;
                    //nextSquare.PieceCount = 1;

                    //Console.WriteLine($"Du står på ruta {nextSquare.ID}.");

                    return true;
                }
            }

            //initialSquare.OccupiedBy = null;
            //initialSquare.PieceCount = 0;

            //nextSquare.OccupiedBy = player;
            //nextSquare.PieceCount = 1;

            //Console.WriteLine($"Du står på ruta {nextSquare.ID}.");
            return true;
        }
        #endregion
    }
}
