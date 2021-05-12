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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ludo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gameRepository;

        public GamesController(LudoContext context, IGamesRepository gameRepository)
        {
            _context = context;
            _gameRepository = gameRepository;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<IEnumerable<Gameboard>> GetAll()
        {
            var gameboards = await _gameRepository.GetAllGames(_context);
            return gameboards;
        }

        // GET api/Games/{id}
        [HttpGet("{id}")]
        public async Task<Gameboard> Get(int id)
        {
            var gameboard = await _gameRepository.GetGame(_context, id);
            return gameboard;
        }

        // POST api/Games/New
        [HttpPost("[action]")]
        [ActionName("New")]
        public async Task<ActionResult<string>> Post([FromBody] List<PlayerDTO> players)
        {
            List<Player> newPlayers = new();
            Gameboard.CreateTracks();

            foreach (PlayerDTO p in players)
            {
                if (string.IsNullOrWhiteSpace(p.Name) || p.Name.Length > 20)
                {
                    return BadRequest("Please enter a name, must be less than 20 characters");
                }

                var color = Color.FromArgb(p.Color);

                if (color == Color.Empty)
                {
                    return BadRequest("Invalid color selection");
                }

                newPlayers.Add(new(p.Name, color));
            }

            var gameboard = await _gameRepository.CreateNewGame(_context, new Gameboard(newPlayers));
            return Ok(gameboard.GameId);
        }

        // PUT api/Games/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<Games>/{id}
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _gameRepository.DeleteGame(_context, id);
        }
    }
}
