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
        private readonly IGameRepository _gameRepository;
        public Gameboard Gameboard { get; }

        public Game(IGameRepository gameRepository, Gameboard gameboard)
        {
            _gameRepository = gameRepository;
            Gameboard = gameboard;
        }

        #region ILudoGame
        public void GetPossibleMoves(Models.Player player, int diceNumber)
        {
            List<Square> moveableTokens = new();

            foreach (var square in Gameboard.Squares.Where(gameboard => gameboard.OccupiedBy == player))
            {
                if (CanMoveToken(player, square, diceNumber))
                {
                    moveableTokens.Add(square);
                }
            }
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

        public bool CanMoveToken(Player player, Square startSquare, int diceNumber)
        {
            int startIndex = player.Track.FindIndex(x => x == startSquare.ID);
            Square initialSquare = Gameboard.Squares[startSquare.ID];

            for (int i = 1; i <= diceNumber; i++)
            {
                int currentIndex = player.Track[startIndex + i];
                Square currentSquare = Gameboard.Squares[currentIndex];
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
