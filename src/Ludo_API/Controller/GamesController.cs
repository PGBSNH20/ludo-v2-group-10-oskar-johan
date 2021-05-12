using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ludo_API.Controller
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

        // GET: api/<Games>
        [HttpGet]
        public async Task<IEnumerable<Gameboard>> GetAll()
        {
            var gameboards = await _gameRepository.GetAllGames(_context);
            return gameboards;
        }

        // GET api/<Games>/5
        [HttpGet("{id}")]
        public async Task<Gameboard> Get(int id)
        {
            var gameboard = await _gameRepository.GetGame(_context, id);
            return gameboard;
        }

        // POST api/<Games>
        [HttpPost("[action]")]
        [ActionName("New")]
        public async Task<int> Post([FromBody] string value)
        {
            var gameboard = await _gameRepository.CreateNewGame(_context);
            return gameboard.ID;
        }

        // PUT api/<Games>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<Games>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _gameRepository.DeleteGame(_context, id);
        }
    }
}
