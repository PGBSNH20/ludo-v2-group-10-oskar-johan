using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ludo_API_Test
{
    public class TestPlayerRepository : IPlayerRepository
    {
        public List<Player> Players { get; set; } = new();

        public TestPlayerRepository()
        {

        }

        public Task<List<Player>> AddPlayers(LudoContext context, List<Player> players)
        {
            Players.AddRange(players);
            return Task.FromResult(Players);
        }
    }
}