using Ludo_API.Database;
using Ludo_API.GameEngine;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameplayController : ControllerBase
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gamesRepository;
        private readonly IMoveActionsRepository _moveActionsRepository;
        private readonly ITurnManager _turnManager;

        public GameplayController(
            LudoContext context,
            IGamesRepository gameRepository,
            IMoveActionsRepository moveActionsRepository,
            ITurnManager turnManager
            )
        {
            _context = context;
            _gamesRepository = gameRepository;
            _moveActionsRepository = moveActionsRepository;
            _turnManager = turnManager;
        }

        // POST api/Gameplay/RollDie
        [HttpPost("[action]")]
        [ActionName("RollDie")]
        public async Task<ActionResult<List<MoveAction>>> PostRollDie(
            [Required][FromBody] PostRollDieDTO postRollDieDTO
        )
        {
            var gameboard = await _gamesRepository.GetGame(_context, postRollDieDTO.GameId);

            if (gameboard == null || gameboard.Squares.Any(square => square.Tenant?.PieceCount >= 4))
            {
                return NotFound($"Can't find an active game with the id {postRollDieDTO.GameId}");
            }

            var player = gameboard.Players.SingleOrDefault(player => player.ID == postRollDieDTO.PlayerId);

            if (player == null)
            {
                return NotFound($"Can't find player with the id {postRollDieDTO.PlayerId}");
            }

            var moveActions = await _moveActionsRepository.GetMoveActions(_context, gameboard.ID, player.ID);
            TurnDataDTO turnDataDTO = new();

            if (moveActions.Count == 0)
            {
                // Call ITurnManager.HandleTurn which rolls the die and builds a list of MoveActions
                turnDataDTO = _turnManager.HandleTurn(gameboard, player);
                moveActions = await _moveActionsRepository.AddMoveActions(_context, turnDataDTO.MoveActions);
            }

            turnDataDTO.MoveActions = moveActions;
            return Ok(turnDataDTO);
        }

        // POST api/Gameplay/ChooseAction
        [HttpPost("[action]")]
        [ActionName("ChooseAction")]
        public async Task<ActionResult<TurnDataDTO>> PostChooseAction([Required][FromBody] int moveActionId)
        {
            var moveAction = await _moveActionsRepository.GetMoveAction(_context, moveActionId);

            if (moveAction == null)
            {
                return NotFound($"Can't find a move action with id {moveActionId}");
            }

            // If the MoveAction is not valid, delete it and return it's message.
            if (!moveAction.ValidMove)
            {
                await _moveActionsRepository.DeleteMoveAction(_context, moveAction);
                TurnDataDTO turnActionDTO =  new()
                {
                    DieRoll = null,
                    Message = moveAction.Message,
                    MoveActions = null,
                };
                return Ok(turnActionDTO);
            }

            bool success;

            if (moveAction.StartSquare == null && moveAction.DestinationSquare == null)
            {
                // The MoveAction represents "no possible moves". We can let the `if (success)`-action do the work of deleting the MoveAction and starting the next turn.
                success = true;
            }
            else
            {
                success = await _gamesRepository.ExecuteMoveAction(_context, moveAction);
            }

            if (success)
            {
                var gameboard = await _gamesRepository.GetGame(_context, moveAction.GameId);

                if (gameboard == null)
                {
                    return NotFound($"Could not find a game with the id: {moveAction.GameId}");
                }

                await _moveActionsRepository.DeleteMoveActions(_context, moveAction.GameId);
                await _turnManager.StartNextTurnAsync(gameboard, moveAction.DiceRoll);
                TurnDataDTO turnActionDTO = new()
                {
                    DieRoll = null,
                    Message = moveAction.Message,
                    MoveActions = null,
                };
                return Ok(turnActionDTO);
            }

            return BadRequest("Move was unsuccessful.");
        }

        // POST api/Gameplay/GetMoveActions
        [HttpGet("[action]")]
        [ActionName("GetMoveActions")]
        public async Task<ActionResult<List<MoveAction>>> GetMoveActions(
            [Required][FromQuery] int GameId,
            [Required][FromQuery] int PlayerId
        )
        {
            var moveActions = await _moveActionsRepository.GetMoveActions(_context, GameId, PlayerId);
            return Ok(moveActions);
        }
    }
}
