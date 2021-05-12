using Ludo_API.Database;
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
            return await context.Gameboards.ToListAsync();
        }

        public async Task<Gameboard> GetGame(LudoContext context, int id)
        {
            return await context.Gameboards.SingleOrDefaultAsync(g => g.ID == id);
        }

        public async Task<Gameboard> CreateNewGame(LudoContext context)
        {
            var gameboard = new Gameboard();
            context.Gameboards.Add(gameboard);
            var saveOperation = await context.SaveChangesAsync();
            return gameboard;
        }
        public void MoveToken(Player player, Square startSquare, Square endSquare)
        {
            throw new NotImplementedException();
        }

        public Task SaveTurnAsync(Gameboard gameboard, Player player)
        {
            throw new NotImplementedException();
        }
    }
}
