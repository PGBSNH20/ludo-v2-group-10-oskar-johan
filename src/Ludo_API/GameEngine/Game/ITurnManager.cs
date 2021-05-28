using Ludo_API.Models;
using Ludo_API.Models.DTO;
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
        //(int dieRoll, List<MoveAction> moveActions) HandleTurn(Gameboard gameboard, Player player);
        TurnDataDTO HandleTurn(Gameboard gameboard, Player player);
        //void EndTurn();
        int RollDice();
        Task StartNextTurnAsync(Gameboard gameboard, int previousTurnDiceRoll);
        //void HandleTurn();
    }
}
