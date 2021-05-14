using Ludo_API.Database;
using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<List<Player>> AddPlayers(LudoContext context, List<Player> players)
        {
            context.Players.AddRange(players);
            await context.SaveChangesAsync();
            return players;
        }
    }
}
