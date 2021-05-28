using Ludo_API.Database;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
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
            return await context.MoveActions.Include(ma => ma.Player).Include(ma => ma.StartSquare).Include(ma => ma.DestinationSquare).SingleOrDefaultAsync(m => m.Id == moveActionId);
        }

        public async Task<List<MoveAction>> GetMoveActions(LudoContext context, int gameId, int playerId)
        {
            return await context.MoveActions.Include(ma => ma.Player).Include(ma => ma.StartSquare).Include(ma => ma.DestinationSquare).Where(ma => ma.GameId == gameId && ma.PlayerId == playerId).ToListAsync();
        }

        public async Task DeleteMoveActions(LudoContext context, int gameId)
        {
            try
            {
                context.MoveActions.RemoveRange(context.MoveActions.Where(ma => ma.GameId == gameId));
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // logging?
                throw;
            }
        }

        public async Task DeleteMoveAction(LudoContext context, MoveAction moveAction)
        {
            try
            {
                context.MoveActions.Remove(moveAction);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // logging?
                throw;
            }
        }
    }
}
