using Ludo_API.Database;
using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public interface IPlayerRepository
    {
        Task<List<Player>> AddPlayers(LudoContext context, List<Player> players);
    }
}
