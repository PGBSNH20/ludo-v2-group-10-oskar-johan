﻿using Ludo_API.Database;
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
        //[Required][FromBody] int gameId, // fixme: only one [FromBody]
        //[Required][FromBody] int playerId // fixme: only one [FromBody]
            [Required][FromBody] PostRollDieDTO postRollDieDTO
        )
        {
            var game = await _gamesRepository.GetGame(_context, postRollDieDTO.GameId);

            if (game == null || game.Squares.Any(square => square.Tenant?.PieceCount >= 4))
            {
                return NotFound($"Can't find an active game with the id {postRollDieDTO.GameId}");
            }

            var player = game.Players.SingleOrDefault(player => player.ID == postRollDieDTO.PlayerId);

            if (player == null)
            {
                return NotFound($"Can't find player with the id {postRollDieDTO.PlayerId}");
            }

            // Call ITurnManager.HandleTurn which rolls the die and builds a list of MoveActions
            var moveActions = _turnManager.HandleTurn(game, player);

            return Ok(await _moveActionsRepository.AddMoveActions(_context, moveActions));
        }

        // POST api/Gameplay/ChooseAction
        [HttpPost("[action]")]
        [ActionName("ChooseAction")]
        public async Task<ActionResult<string>> PostChooseAction([Required][FromBody] int moveActionId)
        {
            var moveAction = await _moveActionsRepository.GetMoveAction(_context, moveActionId);

            if (moveAction == null)
            {
                // todo: maybe replace NotFound with another error
                // e.g. StatusCode(HttpStatusCode.InternalServerError, "error message");
                return NotFound($"Can't find a move action with id {moveActionId}");
            }

            bool success = await _gamesRepository.ExecuteMoveAction(_context, moveAction);

            await _moveActionsRepository.DeleteMoveActions(_context, moveAction.GameId);

            if (success)
            {
                _turnManager.NextTurn(moveAction.DiceRoll);
                return Ok("Move action was succesfully executed");
            }

            // todo: maybe replace NotFound with another error
            // e.g. StatusCode(HttpStatusCode.InternalServerError, "error message");
            return BadRequest("Move was unsuccessful.");
        }

        // POST api/Gameplay/GetMoveActions
        [HttpGet("[action]")]
        [ActionName("GetMoveActions")]
        public async Task<ActionResult<List<MoveAction>>> GetMoveActions([Required][FromQuery] PostRollDieDTO postRollDieDTO)
        {
            var moveActions = await _moveActionsRepository.GetMoveActions(_context, postRollDieDTO.GameId, postRollDieDTO.PlayerId);

            if (moveActions == null)
            {
                return NotFound($"Can't find a move action for game with id: {postRollDieDTO.GameId} and playerId {postRollDieDTO.PlayerId}");
            }

            return Ok(moveActions);
        }
    }
}
