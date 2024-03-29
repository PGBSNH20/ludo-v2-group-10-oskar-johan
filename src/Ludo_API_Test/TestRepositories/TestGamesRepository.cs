﻿using Ludo_API;
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
                        Tenant = new SquareTenant(i, null, 0),
                    });
                }

                List<Player> players = new()
                {
                    new Player($"Oskar {i + 10}", "Yellow"),
                    new Player( $"Randa {i + 10}", "Red")
                };


                squares[players[i].StartPosition].Tenant = new SquareTenant(players[i].StartPosition, players[i], 1);

                Gameboard gb = new()
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
            return Task.FromResult(Gameboards.SingleOrDefault(g => g.ID == id));
        }

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

        public Task<Gameboard> AddPlayerAsync(LudoContext context, Gameboard gameboard, Player player)
        {
            gameboard.Players.Add(player);
            return Task.FromResult(gameboard);
        }

        public Task StartGameAsync(LudoContext context, Gameboard gameboard)
        {
            gameboard.GameStartDate = DateTime.Now;
            return Task.CompletedTask;
        }

        public Task SaveTurnAsync(LudoContext context, Gameboard gameboard, Player player)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsColorTaken(LudoContext context, int gameboardId, string color)
        {
            var gameboards = Gameboards;
            return Task.FromResult(gameboards.Any(g => g.ID == gameboardId && g.Players.Any(p => p.Color == color)));
        }

        public Task SetCreator(LudoContext context, Gameboard gameboard, Player newPlayer)
        {
            gameboard.GameCreator = newPlayer;
            return Task.CompletedTask;
        }

        public Task SetCurrentPlayer(LudoContext context, Gameboard gameboard, Player player)
        {
            if (gameboard == null)
            {
                throw new ArgumentNullException(nameof(gameboard), "Gameboard is null");
            }

            gameboard.CurrentPlayer = player ?? throw new ArgumentNullException(nameof(player), "Player is null");

            return Task.CompletedTask;
        }
    }
}
