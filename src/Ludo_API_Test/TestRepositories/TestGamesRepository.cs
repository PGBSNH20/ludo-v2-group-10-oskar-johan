using Ludo_API;
using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Ludo_API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_API_Test.TestRepositories
{
    class TestGamesRepository : IGamesRepository
    {
        public List<Gameboard> Gameboards { get; set; } = new();
        public List<Square> Squares { get; set; } = new();

        public TestGamesRepository()
        {
        }

        //public TestGamesRepository(List<Gameboard> gameboards)
        //{
        //    Gameboards = gameboards;
        //}

        //public TestGamesRepository(List<Gameboard> gameboards, List<Square> squares)
        //{
        //    Gameboards = gameboards;
        //    Squares = squares;
        //}

        public Task AddNewGameAsync(Gameboard gameboard, Player players)
        {
            throw new NotImplementedException();
        }

        public Task SaveTurnAsync(Gameboard gameboard, Player player)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gameboard>> GetAllGamesAsync()
        {

            for (int i = 0; i < 2; i++)
            {
                var squares = new List<Square>();

                for (int j = 0; j < 60; j++)
                {
                    squares.Add(new Square
                    {
                        ID = j,
                        //PieceCount = 0
                        Tenant = new SquareTenant(i, null, 0),
                    });
                }

                List<Player> players = new List<Player>
                {
                    new Player($"Oskar {i + 10}", Color.Yellow),
                    new Player( $"Randa {i + 10}", Color.Red)
                };

                //squares[players[i].StartPosition].Tenant?.Player = players[i];
                //squares[players[i].StartPosition].Tenant?.PieceCount = 1;
                squares[players[i].StartPosition].Tenant = new SquareTenant(players[i].StartPosition, players[i], 1);

                Gameboard gb = new Gameboard()
                {
                    ID = i,
                    LastPlayer = players[i],
                    Squares = squares,
                    GameDate = DateTime.Now,
                    Players = players
                };

                Gameboards.Add(gb);
            }

            return Task.FromResult(Gameboards);
        }

        public Task<Gameboard> CreateNewGame(LudoContext context, Gameboard gameboard)
        {
            Gameboards.Add(gameboard);
            gameboard.Squares.ForEach(s =>
            {
                s.Gameboard = gameboard;
                s.GameboardId = gameboard.ID;
            });
            Squares = gameboard.Squares;
            return Task.FromResult(gameboard);
        }

        public Task<bool> DeleteGame(LudoContext context, int id)
        {
            var gameboard = Gameboards.SingleOrDefault(g => g.ID == id);

            if (gameboard == null)
            {
                return Task.FromResult(false);
            }

            Gameboards.Remove(gameboard);
            return Task.FromResult(true);
        }

        public Task<List<Gameboard>> GetAllGames(LudoContext context)
        {
            return Task.FromResult(Gameboards);
        }

        public Task<Gameboard> GetGame(LudoContext context, int id)
        {
            throw new NotImplementedException();
        }

        //public void MoveToken(Player player, Square startSquare, Square endSquare)
        //{
        //    startSquare.Tenant = new SquareTenant(startSquare.ID, null, 0);
        //    //startSquare.Tenant?.Player = null;
        //    //startSquare.Tenant?.PieceCount = 0;

        //    startSquare.Tenant = new SquareTenant(startSquare.ID, player, (endSquare.Tenant?.PieceCount).GetValueOrDefault() + 1);
        //    //endSquare.Tenant?.Player = player;
        //    //endSquare.Tenant?.PieceCount++;
        //}

        //public Task<bool> ExecuteMoveAction(LudoContext context, MoveAction moveAction)
        public Task<bool> ExecuteMoveAction(LudoContext context, MoveAction moveAction)
        {
            if (moveAction.StartSquare != null)
            {
                    var startSquare = Squares.SingleOrDefault(s => s.ID == moveAction.StartSquare?.SquareIndex);

                if (startSquare != null)
                {
                    startSquare.Tenant = moveAction.StartSquare;
                }
            }

            if (moveAction.DestinationSquare != null)
            {
                var destinationSquare = Squares.SingleOrDefault(s => s.ID == moveAction.DestinationSquare.SquareIndex);

                if (destinationSquare == null)
                {
                    return Task.FromResult(false);
                }

                destinationSquare.Tenant = moveAction.DestinationSquare;
            }

            return Task.FromResult(true);
        }
    }
}
