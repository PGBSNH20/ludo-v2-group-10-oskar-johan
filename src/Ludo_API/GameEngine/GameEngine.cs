using Ludo_API.GameEngine.Game;
using Game = Ludo_API.GameEngine.Game.Game;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludo_API.Database;

namespace Ludo_API.GameEngine
{
    public class GameEngine : IGameEngine
    {
        private readonly LudoContext _context;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        //private readonly IGameEngine _gameEngine;

        //internal GameEngine(IGameRepository gameRepository, IPlayerRepository playerRepository, IGameEngine gameEngine)
        public GameEngine(LudoContext context, IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            _context = context;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            //_gameEngine = gameEngine;
        }

        #region IGameEngine
        public void EndGame()
        {
            throw new NotImplementedException();
        }
        public void GetRules()
        {
            throw new NotImplementedException();
        }

        public void LoadGame(int gameId)
        {
            //var game = _gameRepository.GetGame(id: gameId);
            //RunGame(game);

            throw new NotImplementedException();
        }

        public void NewGame(GameOptions gameOptions)
        {
            throw new NotImplementedException();
            //var playerManager = new PlayerManager();
            Gameboard gameboard = new()
            {
                Players = new PlayerManager().CreatePlayers(_context, _playerRepository, gameOptions),
            };
            //var game = new Game();
        }

        public void RunGame(Game.Game game)
        {
            throw new NotImplementedException();
            //var turnManager = new TurnManager();
            //turnManager.Start();
        }
        #endregion
    }
}
