using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public interface IGamesRepository
    {
        Task AddNewGameAsync(Gameboard gameboard, Player players);
        Task<List<Gameboard>> GetAllGamesAsync();
        Task<List<Gameboard>> GetAllGames(LudoContext context);
        Task<Gameboard> GetGame(LudoContext context, int id);
        Task<Gameboard> CreateNewGame(LudoContext context, Gameboard gameboard);
        Task StartGameAsync(LudoContext context, Gameboard gameboard);
        Task SaveTurnAsync(LudoContext context, Gameboard gameboard, Player player);
        //void MoveToken(Player player, Square startSquare, Square endSquare);
        Task<bool> DeleteGame(LudoContext context, int id);
        Task<bool> ExecuteMoveAction(LudoContext context, MoveAction moveAction);
        Task<Gameboard> AddPlayerAsync(LudoContext context, Gameboard gameboard, Player player);
        Task<bool> IsColorTaken(LudoContext context, int gameboardId, string color);
        Task SetCreator(LudoContext context, Gameboard gameboard, Player newPlayer);
        Task SetCurrentPlayer(LudoContext context, Gameboard gameboard, Player player);
    }
}
