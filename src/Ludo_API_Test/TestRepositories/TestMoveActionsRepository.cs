using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API_Test
{
    public class TestMoveActionsRepository : IMoveActionsRepository
    {
        List<MoveAction> MoveActions { get; set; } = new();

        public Task<List<MoveAction>> AddMoveActions(LudoContext context, List<MoveAction> moveActions)
        {
            MoveActions.AddRange(moveActions);

            return Task.FromResult(moveActions);
        }

        public Task DeleteMoveAction(LudoContext context, MoveAction moveAction)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMoveActions(LudoContext context, int gameId)
        {
            throw new System.NotImplementedException();
        }

        public Task<MoveAction> GetMoveAction(LudoContext context, int moveActionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MoveAction>> GetMoveActions(LudoContext context, int gameId, int playerId)
        {
            return Task.FromResult(MoveActions.Where(ma => ma.GameId == gameId && ma.PlayerId == playerId).ToList());
        }
    }
}