using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
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
            return await context.Gameboards.ToListAsync();
        }

        public async Task<Gameboard> GetGame(LudoContext context, int id)
        {
            return await context.Gameboards.
                Include(s => s.Squares).
                Include(p => p.Players).
                SingleOrDefaultAsync(g => g.ID == id);
        }

        public async Task<Gameboard> CreateNewGame(LudoContext context, Gameboard gameboard)
        {
            context.Gameboards.Add(gameboard);
            var saveOperation = await context.SaveChangesAsync();
            return gameboard;
        }
        //public void MoveToken(Player player, Square startSquare, Square endSquare)
        //{
        //    throw new NotImplementedException();
        //}

        public Task SaveTurnAsync(Gameboard gameboard, Player player)
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
                var startSquare = await context.Squares.SingleOrDefaultAsync(s => s.ID == moveAction.StartSquare.SquareIndex);

                if (startSquare != null)
                {
                    startSquare.Tenant = moveAction.StartSquare;
                }
            }

            if (moveAction.DestinationSquare != null)
            {
                var destinationSquare = await context.Squares.SingleOrDefaultAsync(s => s.ID == moveAction.DestinationSquare.SquareIndex);

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

        public Task AddPlayerAsync(Gameboard gameboard, NewPlayerDTO newPlayerDTO)
        {
            gameboard.Players.Add(new)
        }
    }
}