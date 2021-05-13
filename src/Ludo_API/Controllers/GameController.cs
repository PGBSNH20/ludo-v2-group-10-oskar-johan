using Ludo_API.Database;
using Ludo_API.GameEngine;
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
    public class GameController : ControllerBase
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gamesRepository;
        private readonly ITurnBased _turnManager;

        public GameController(LudoContext context, IGamesRepository gameRepository, ITurnBased turnManager)
        {
            _context = context;
            _gamesRepository = gameRepository;
            _turnManager = turnManager;
        }

        // POST api/Games/New
        //[HttpGet("[action]")]
        //public async Task<ActionResult<int>> Get...(
        //    [Required][FromBody] int gameId,
        //    [Required][FromBody] int playerId
        //)
        //{
        //}

        // POST api/Games/New
        //[HttpPost("[action]")]
        //[ActionName("RollDie")]
        //public async Task<ActionResult<PossibleMoveDTO[]>> Post(
        //    [Required][FromBody] int gameId,
        //    [Required][FromBody] int playerId
        //)
        //{
        //    var game = await _gamesRepository.GetGame(_context, gameId);

        //    if (game == null || game.Squares.Any(square => square.PieceCount >= 4))
        //    {
        //        return NotFound($"Can't find an active game with the id {gameId}");
        //    }

        //    var player = game.Players.SingleOrDefault(player => player.ID == playerId);

        //    if (player == null)
        //    {
        //        return NotFound($"Can't find player with the id {playerId}");
        //    }

        //    // HandleTurn implies that the entire turn is handled when called?
        //    _turnManager.HandleTurn(player);
        //    // So maybe it's better to make individual calls anyway?
        //    //int dieRoll = _turnManager.RollDice();
        //    //_game.GetPossibleMoves(player, diceNumber);

        //    return new List; //  temp
        //}
    }
}
