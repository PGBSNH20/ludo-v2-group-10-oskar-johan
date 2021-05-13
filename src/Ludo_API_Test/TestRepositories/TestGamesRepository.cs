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
        public List<Gameboard> Gameboards { get; set; } = new();

        public TestGamesRepository()
        {
        }

        public TestGamesRepository (List<Gameboard> gameboards)
        {
            Gameboards = gameboards;
        }

        public Task<Gameboard> CreateNewGame(LudoContext context, Gameboard gameboard)
        {
            Gameboards.Add(gameboard);
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
