using Ludo_API.Models;
using System.Collections.Generic;

namespace Ludo_API.GameEngine
{
    public interface ITurnBased
    {
        Player DecideWhoStarts(List<Player> players);
        void NextTurn();
        void HandleTurn(Models.Player player);
        void EndTurn();
        int RollDice();
        void HandleTurn();
    }
}
