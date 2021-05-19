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

        //// POST api/Games/New
        //[HttpPost("[action]")]
        //[ActionName("New")]
        ////public async Task<ActionResult<string>> Post([FromBody] List<PlayerDTO> players)
        //public async Task<ActionResult<int>> Post([FromBody] List<PlayerDTO> players)
        //{
        //    List<Player> newPlayers = new();
        //    Gameboard.CreateTracks();

        //    foreach (PlayerDTO p in players)
        //    {
        //        //if (string.IsNullOrWhiteSpace(p.Name) || p.Name.Length > 20)
        //        //{
        //        //    return BadRequest("Please enter a name, must be less than 20 characters");
        //        //}

        //        var color = ColorTranslator.FromHtml(p.Color);
        //        //var color = Color.FromArgb(p.Color);

        //        //if (color.ToArgb() == Color.Empty.ToArgb())
        //        //{
        //        //    return BadRequest("Invalid color selection");
        //        //}

        //        newPlayers.Add(new(p.Name, color));
        //    }

        //    var gameboard = await _gameRepository.CreateNewGame(_context, new Gameboard(newPlayers));
        //    //gameboard.SetPlayerColors();
        //    return Ok(gameboard.GameId);
        //}

        // POST api/Games/New
        [HttpPost("[action]")]
        [ActionName("New")]
        //public async Task<ActionResult<string>> Post([FromBody] List<PlayerDTO> players)
        public async Task<ActionResult<int>> Post([FromBody] NewPlayerDTO newPlayerDTO)
        //public async Task<ActionResult<NewGameDTO>> Post([FromBody] NewGameDTO newGameDTO)
        {
            List<Player> newPlayers = new();
            Gameboard.CreateTracks();

            //foreach (PlayerDTO p in players)
            //{
            //    //if (string.IsNullOrWhiteSpace(p.Name) || p.Name.Length > 20)
            //    //{
            //    //    return BadRequest("Please enter a name, must be less than 20 characters");
            //    //}

            //    var color = ColorTranslator.FromHtml(p.Color);
            //    //var color = Color.FromArgb(p.Color);

            //    //if (color.ToArgb() == Color.Empty.ToArgb())
            //    //{
            //    //    return BadRequest("Invalid color selection");
            //    //}

            //    newPlayers.Add(new(p.Name, color));
            //}

            var color = ColorTranslator.FromHtml(newPlayerDTO.PlayerColor);
            newPlayers.Add(new(newPlayerDTO.PlayerName, color));

            try
            {
                var gameboard = new Gameboard(newPlayers);
                gameboard = await _gameRepository.CreateNewGame(_context, gameboard);
                //gameboard.SetPlayerColors();
                return Ok(gameboard.ID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //return Ok(1);
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

        // POST api/Games/AddPlayer
        [HttpPost("[action]")]
        [ActionName("AddPlayer")]
        //public async Task<ActionResult<string>> Post([FromBody] List<PlayerDTO> players)
        public async Task<ActionResult<int>> PostAddPlayer([FromBody] NewPlayerDTO newPlayerDTO)
        //public async Task<ActionResult<NewGameDTO>> Post([FromBody] NewGameDTO newGameDTO)
        {
            var gameboard = _gameRepository.GetGame(_context, newPlayerDTO.GameId);

            






            List<Player> newPlayers = new();
            Gameboard.CreateTracks();

            var color = ColorTranslator.FromHtml(newPlayerDTO.PlayerColor);
            newPlayers.Add(new(newPlayerDTO.PlayerName, color));

            try
            {
                var gameboard = new Gameboard(newPlayers);
                gameboard = await _gameRepository.CreateNewGame(_context, gameboard);
                //gameboard.SetPlayerColors();
                return Ok(gameboard.ID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //return Ok(1);
        }
    }
