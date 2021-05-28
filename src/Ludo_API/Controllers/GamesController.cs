using Ludo_API.Data;
using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using Ludo_API.GameEngine.Game;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ludo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gameRepository;
        private readonly ITurnManager _turnManager;

        public GamesController(LudoContext context, IGamesRepository gameRepository, ITurnManager turnManager)
        {
            _context = context;
            _gameRepository = gameRepository;
            _turnManager = turnManager;
        }

        /// <summary>
        /// Get all gameboards.
        /// </summary>
        /// <returns>An IEnumerable of Gameboards.</returns>
        // GET: api/Games
        [HttpGet]
        public async Task<IEnumerable<GameboardDTO>> GetAll()
        {
            var gameboards = await _gameRepository.GetAllGames(_context);

            return gameboards.Select(g => new GameboardDTO(g));
            //return gameboards;
        }

        /// <summary>
        /// Get a gameboard by its ID.
        /// </summary>
        /// <param name="id">int: The ID of the gameboard.</param>
        /// <returns>A GameboardDTO object.</returns>
        // GET api/Games/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GameboardDTO>> Get([FromRoute] int id)
        {
            var gameboard = await _gameRepository.GetGame(_context, id);

            if (gameboard == null)
            {
                return NotFound("No game with that id exists");
            }

            return Ok(new GameboardDTO(gameboard));
        }

        /// <summary>
        /// Get Ludo data such as gameboard layout, player colors and their gameboard track indices.
        /// </summary>
        /// <returns>A GameboardDTO object.</returns>
        // GET api/Games/LudoData
        [HttpGet("[action]")]
        [ActionName("LudoData")]
        public ActionResult<LudoData> GetLudoData()
        {
            if (LudoData.Instance == null)
            {
                // todo: logging?
                return NotFound("Could not find the LudoData resources.");
            }

            return LudoData.Instance;
        }

        /// <summary>
        /// Create a new game.
        /// </summary>
        /// <param name="newPlayerDTO">A NewPlayerDTO object containing the name and chosen id of the player creating the game.</param>
        /// <returns>int: ID of the new gameboard whichs represents a game.</returns>
        // POST api/Games/New
        [HttpPost("[action]")]
        [ActionName("New")]
        public async Task<ActionResult<GameboardDTO>> Post([FromBody] NewPlayerDTO newPlayerDTO)
        {
            try
            {
                Gameboard.CreateTracks();

                var newPlayer = new Player(newPlayerDTO);
                List<Player> newPlayers = new() {
                    newPlayer
                };

                // Save the gameboard.
                var gameboard = new Gameboard(newPlayers);
                gameboard = await _gameRepository.CreateNewGame(_context, gameboard);
                await _gameRepository.SetCreator(_context, gameboard, newPlayer);

                return Ok(new GameboardDTO(gameboard));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes game from database specefied by ID
        /// </summary>
        /// <param name="id">ID provided for game to be deleted</param>
        /// <returns>void</returns>
        // DELETE api/Games/{id}
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _gameRepository.DeleteGame(_context, id);
        }

        /// <summary>
        /// Add a new player to a game.
        /// </summary>
        /// <param name="newPlayerDTO">A NewPlayerDTO object containing the name and color the player.</param>
        /// <returns>Returns a Gameboard object.</returns>
        // POST api/Games/AddPlayer
        [HttpPost("[action]")]
        [ActionName("AddPlayer")]
        public async Task<ActionResult<NewPlayerDTO>> PostAddPlayer([FromBody] NewPlayerDTO newPlayerDTO)
        {
            if (newPlayerDTO.GameId == null)
            {
                return BadRequest("Game ID is required");
            }

            try
            {
                var gameboard = await _gameRepository.GetGame(_context, newPlayerDTO.GameId.Value);

                if (gameboard == null)
                {
                    return NotFound($"Could not find a game with the id: {newPlayerDTO.GameId}");
                }

                Gameboard.CreateTracks();

                if (await _gameRepository.IsColorTaken(_context, gameboard.ID, newPlayerDTO.PlayerColor))
                {
                    return BadRequest($"The color {newPlayerDTO.PlayerColor} is used by another player, please try again.");
                }

                Player player = new(newPlayerDTO);
                gameboard = await _gameRepository.AddPlayerAsync(_context, gameboard, player);

                newPlayerDTO.ID = player.ID;

                return Ok(newPlayerDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Start a game.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <returns>A GameboardDTO object.</returns>
        // POST api/Games/StartGame
        [HttpPost("[action]")]
        [ActionName("StartGame")]
        public async Task<IActionResult> PostStartGame([FromBody] int gameId)
        {
            try
            {
                var gameboard = await _gameRepository.GetGame(_context, gameId);

                if (gameboard == null)
                {
                    return NotFound($"There is no game with the id {gameId}.");
                }

                await _turnManager.StartGameAsync(gameboard);
                return Ok(new GameboardDTO(gameboard));
            }
            catch (Exception)
            {
                // log exception?
                return BadRequest("Something went wrong.");
                throw;
            }
        }
    }
}
