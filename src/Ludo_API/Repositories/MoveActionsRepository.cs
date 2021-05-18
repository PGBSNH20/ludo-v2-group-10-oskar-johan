using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Repositories
{
    public class MoveActionsRepository : IMoveActionsRepository
    {
        public async Task<List<MoveAction>> AddMoveActions(LudoContext context, List<MoveAction> moveActions)
        {
            context.MoveActions.AddRange(moveActions);
            await context.SaveChangesAsync();

            return moveActions;
        }

        public async Task<MoveAction> GetMoveAction(LudoContext context, int moveActionId)
        {
            return await context.MoveActions.SingleOrDefaultAsync(m => m.Id == moveActionId);
        }
    }
}
