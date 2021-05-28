using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class TurnManager : ITurnManager
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gameRepository;
        private readonly IDie _die;
        private readonly Game _game;

        public TurnManager(LudoContext context, IGamesRepository gamesRepository, Game game, IDie die)
        {
            _context = context;
            _gameRepository = gamesRepository;
            _game = game;
            _die = die;
        }

        public Player DecideWhoStarts(Gameboard gameboard)
        {
            var startnumber = new Random();
            var start = startnumber.Next(0, gameboard.Players.Count);
            return gameboard.Players.ElementAt(start);
        }

        public Player GetNextPlayer(Gameboard gameboard)
        {
            var playerColors = gameboard.Players.Select(p => p.Color);
            var validColors = Player.GetValidColors().Where(c => playerColors.Contains(c)).ToList();

            int currentColorIndex = validColors.FindIndex(color => color == gameboard.CurrentPlayer.Color);

            string nextColor = validColors[currentColorIndex + 1 < validColors.Count ? currentColorIndex + 1 : 0];
            return gameboard.Players.Single(p => p.Color == nextColor);
        }

        public async Task StartNextTurnAsync(Gameboard gameboard, int previousTurnDiceRoll)
        {
            if (previousTurnDiceRoll == 6)
            {
                // todo: do something?
            }
            else
            {
                var nextPlayer = GetNextPlayer(gameboard);
                await _gameRepository.SetCurrentPlayer(_context, gameboard, nextPlayer);
            }
        }

        public TurnDataDTO HandleTurn(Gameboard gameboard, Player player)
        {
            int diceNumber = RollDice();
            return new TurnDataDTO
            {
                DieRoll = diceNumber,
                MoveActions = _game.GetPossibleMoves(gameboard, player, diceNumber),
            };
        }

        public int RollDice()
        {
            return _die.RollDie();
        }

        public async Task StartGameAsync(Gameboard gameboard)
        {
            var player = DecideWhoStarts(gameboard);
            await _gameRepository.StartGameAsync(_context, gameboard);
            await _gameRepository.SetCurrentPlayer(_context, gameboard, player);
        }
    }
}
