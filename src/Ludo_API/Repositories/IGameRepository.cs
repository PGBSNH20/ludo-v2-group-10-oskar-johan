using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public interface IGameRepository
    {
        Task SaveTurnAsync(Gameboard gameboard, Player player);
        void MoveToken(Player player, Square startSquare, Square endSquare);
    }
}
