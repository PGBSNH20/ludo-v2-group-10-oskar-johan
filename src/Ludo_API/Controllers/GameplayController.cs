using Ludo_API.Database;
using Ludo_API.GameEngine;
using Ludo_API.GameEngine.Game;
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
        private readonly IMoveActionsRepository _moveActionRepository;
        private readonly ITurnBased _turnManager;

        public GameplayController(
            LudoContext context,
            IGamesRepository gameRepository,
            IMoveActionsRepository moveActionRepository,
            ITurnBased turnManager
            )
        {
            _context = context;
            _gamesRepository = gameRepository;
            _moveActionRepository = moveActionRepository;
            _turnManager = turnManager;
        }

        // POST api/Gameplay/New
        //[HttpGet("[action]")]
        //public async Task<ActionResult<int>> Get...(
        //    [Required][FromBody] int gameId,
        //    [Required][FromBody] int playerId
        //)
        //{
        //}

        // POST api/Gameplay/RollDie
        [HttpPost("[action]")]
        [ActionName("RollDie")]
        public async Task<ActionResult<List<MoveAction>>> PostRollDie(
            [Required][FromBody] int gameId,
            [Required][FromBody] int playerId
        )
        {
            var game = await _gamesRepository.GetGame(_context, gameId);

            if (game == null || game.Squares.Any(square => square.PieceCount >= 4))
            {
                return NotFound($"Can't find an active game with the id {gameId}");
            }

            var player = game.Players.SingleOrDefault(player => player.ID == playerId);

            if (player == null)
            {
                return NotFound($"Can't find player with the id {playerId}");
            }

            var moveActions = _turnManager.HandleTurn(player);

            return Ok(await _moveActionRepository.AddMoveActions(_context, moveActions));
        }

        // POST api/Gameplay/ChoseAction
        [HttpPost("[action]")]
        [ActionName("ChooseAction")]
        public async Task<ActionResult<bool>> PostChooseAction(
            [Required][FromBody] int moveActionId,
            [Required][FromBody] int playerId
            )
        {
            var moveAction = await _moveActionRepository.GetMoveAction(_context, moveActionId);

            if (moveAction == null)
            {
                // todo: maybe replace NotFound with another error
                // e.g. StatusCode(HttpStatusCode.InternalServerError, "error message");
                return NotFound($"Can't find a move action with id {moveActionId}");
            }

            bool success = await _gamesRepository.ExecuteMoveAction(_context, moveAction);

            if (success)
            {
                return Ok();
            }

            // todo: maybe replace NotFound with another error
            // e.g. StatusCode(HttpStatusCode.InternalServerError, "error message");
            return BadRequest("Move unsuccessful");
        }
    }
}
