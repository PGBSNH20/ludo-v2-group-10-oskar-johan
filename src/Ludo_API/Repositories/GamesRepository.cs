﻿using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        public async Task<List<Gameboard>> GetAllGames(LudoContext context)
        {
            return await context.Gameboards
                .Include(s => s.Squares)
                .ThenInclude(t => t.Tenant)
                .Include(p => p.Players)
                .ToListAsync();
        }

        public async Task<Gameboard> GetGame(LudoContext context, int id)
        {
            var gameboard = await context.Gameboards
                .Include(s => s.Squares)
                .ThenInclude(t => t.Tenant)
                .Include(p => p.Players)
                .SingleOrDefaultAsync(g => g.ID == id);
            return gameboard;
        }

        public async Task<Gameboard> CreateNewGame(LudoContext context, Gameboard gameboard)
        {
            context.Gameboards.Add(gameboard);
            await context.SaveChangesAsync();
            return await context.Gameboards.SingleOrDefaultAsync(g => g == gameboard);
        }

        public async Task StartGameAsync(LudoContext context, Gameboard gameboard)
        {
            gameboard.GameStartDate = DateTime.Now;
            await context.SaveChangesAsync();
        }

        public Task SaveTurnAsync(LudoContext context, Gameboard gameboard, Player player)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteGame(LudoContext context, int id)
        {
            var gameboard = await context.Gameboards.SingleOrDefaultAsync(g => g.ID == id);

            if (gameboard == null)
            {
                return false;
            }

            context.Gameboards.Remove(gameboard);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExecuteMoveAction(LudoContext context, MoveAction moveAction)
        {
            if (moveAction.StartSquare != null)
            {
                Square startSquare;
                try
                {
                    startSquare = await context.Squares.SingleOrDefaultAsync(s => s.GameboardId == moveAction.GameId && s.ID == moveAction.StartSquare.SquareIndex);
                }
                catch (Exception)
                {
                    throw new Exception("Error retreiving square matching moveAction.StartSquare!");
                }

                if (startSquare != null)
                {
                    startSquare.Tenant = moveAction.StartSquare;
                }
            }

            if (moveAction.DestinationSquare != null)
            {
                Square destinationSquare;
                try
                {
                    destinationSquare = await context.Squares.SingleOrDefaultAsync(s => s.GameboardId == moveAction.GameId && s.ID == moveAction.DestinationSquare.SquareIndex);
                }
                catch (Exception)
                {
                    throw new Exception("Error retreiving square matching moveAction.DestinationSquare!");
                }

                if (destinationSquare == null)
                {
                    return false;
                }

                destinationSquare.Tenant = moveAction.DestinationSquare;
            }

            await context.SaveChangesAsync();

            return true;
        }

        public Task AddNewGameAsync(Gameboard gameboard, Player players)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gameboard>> GetAllGamesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Gameboard> AddPlayerAsync(LudoContext context, Gameboard gameboard, Player player)
        {
            gameboard.Players.Add(player);
            await context.SaveChangesAsync();

            return gameboard;
        }

        public async Task<bool> IsColorTaken(LudoContext context, int gameboardId, string color)
        {
            var gameboards = context.Gameboards.Include(g => g.Players);
            return await gameboards.AnyAsync(g => g.ID == gameboardId && g.Players.Any(p => p.Color == color));
        }

        public async Task SetCreator(LudoContext context, Gameboard gameboard, Player newPlayer)
        {
            gameboard.GameCreator = newPlayer;
            await context.SaveChangesAsync();
        }

        public async Task SetCurrentPlayer(LudoContext context, Gameboard gameboard, Player player)
        {
            if (gameboard == null)
            {
                throw new ArgumentNullException(nameof(gameboard), "Gameboard is null");
            }

            gameboard.CurrentPlayer = player ?? throw new ArgumentNullException(nameof(player), "Player is null");

            await context.SaveChangesAsync();
        }
    }
}
