using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ludo_API
{
    public interface IMoveActionsRepository
    {
        Task<List<MoveAction>> AddMoveActions(LudoContext context, List<MoveAction> moveActions);
        Task<MoveAction> GetMoveAction(LudoContext context, int moveActionId);
    }
}