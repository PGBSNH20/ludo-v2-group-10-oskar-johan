using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;

namespace Ludo_API.Tests
{
    public class GamesRepositoryTest : IGamesRepository
    {
        private List<Gameboard> _gameBoards = new();

        public async Task AddNewGameAsync(Gameboard gameboard, Player players)
        {
        }

        public async Task SaveTurnAsync(Gameboard gameboard, Player player)
        {
        }

        public async Task<List<Gameboard>> GetAllGamesAsync()
        {

            for (int i = 0; i < 2; i++)
            {
                var squares = new List<Square>();

                for (int j = 0; j < 60; j++)
                {
                    squares.Add(new Square
                    {
                        ID = j,
                        PieceCount = 0
                    });
                }

                List<Player> players = new List<Player>
                {
                    new Player($"Oskar {i + 10}", Color.Yellow),
                    new Player( $"Randa {i + 10}", Color.Red)
                };

                squares[players[i].StartPosition].OccupiedBy = players[i];
                squares[players[i].StartPosition].PieceCount = 1;

                Gameboard gb = new Gameboard()
                {
                    ID = i,
                    LastPlayer = players[i],
                    Squares = squares,
                    GameDate = DateTime.Now,
                    Players = players
                };

                _gameBoards.Add(gb);
            }

            return _gameBoards;
        }

        public void MoveToken(Player player, Square startSquare, Square endSquare)
        {
            startSquare.OccupiedBy = null;
            startSquare.PieceCount = 0;

            endSquare.OccupiedBy = player;
            endSquare.PieceCount++;
        }

        public Task CreateNewGame(LudoContext context)
        {
            throw new NotImplementedException();
        }
    }
}
