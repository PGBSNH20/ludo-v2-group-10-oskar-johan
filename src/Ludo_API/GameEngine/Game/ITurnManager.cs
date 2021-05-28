using Ludo_API.Models;
using Ludo_API.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public interface ITurnManager
    {
        Task StartGameAsync(Gameboard gameboard);
        Player DecideWhoStarts(Gameboard gameboard);
        TurnDataDTO HandleTurn(Gameboard gameboard, Player player);
        int RollDice();
        Task StartNextTurnAsync(Gameboard gameboard, int previousTurnDiceRoll);
    }
}
