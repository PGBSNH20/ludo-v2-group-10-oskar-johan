using Ludo_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public interface ITurnManager
    {
        Task StartGameAsync(Gameboard gameboard);
        //Player DecideWhoStarts(List<Player> players);
        Player DecideWhoStarts(Gameboard gameboard);
        //void NextTurn();
        List<MoveAction> HandleTurn(Gameboard gameboard, Player player);
        //void EndTurn();
        int RollDice();
        //void HandleTurn();
    }
}
