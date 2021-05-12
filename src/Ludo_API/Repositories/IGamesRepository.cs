using Ludo_API.Database;
using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public interface IGamesRepository
    {
        Task<List<Gameboard>> GetAllGames(LudoContext context);
        Task<Gameboard> GetGame(LudoContext context, int id);
        Task<Gameboard> CreateNewGame(LudoContext context);
        Task SaveTurnAsync(Gameboard gameboard, Player player);
        void MoveToken(Player player, Square startSquare, Square endSquare);
        Task<bool> DeleteGame(LudoContext context, int id);
    }
}
