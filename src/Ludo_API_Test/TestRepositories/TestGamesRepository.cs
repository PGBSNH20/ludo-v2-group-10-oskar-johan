using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_API_Test.TestRepositories
{
    class TestGamesRepository : IGamesRepository
    {
        public List<Gameboard> Gameboards { get; set; }

        public Task<Gameboard> CreateNewGame(LudoContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGame(LudoContext context, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gameboard>> GetAllGames(LudoContext context)
        {
            throw new NotImplementedException();
        }

        public Task<Gameboard> GetGame(LudoContext context, int id)
        {
            throw new NotImplementedException();
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
