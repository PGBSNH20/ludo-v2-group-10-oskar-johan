using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{

    public class TurnManager : ITurnManager
    //internal class TurnManager
    {
        private readonly LudoContext _context;
        private readonly IGamesRepository _gameRepository;
        //private readonly Random _die;
        private readonly IDie _die;
        private readonly Game _game;

        public TurnManager(LudoContext context, IGamesRepository gamesRepository, Game game, IDie die)
        {
            _context = context;
            _gameRepository = gamesRepository;
            _game = game;

            //_die = new Random();
            _die = die;
        }

        //public TurnManager(Game game)
        //{
        //    _die = new Random();
        //}

        #region ITurnManager
        //public Player DecideWhoStarts(List<Player> players)
        public Player DecideWhoStarts(Gameboard gameboard)
        {
            var startnumber = new Random();
            var start = startnumber.Next(0, gameboard.Players.Count);
            return gameboard.Players.ElementAt(start);
        }

        //public Player GetNextPlayer(Player player)
        //{
            // update to use repo
            // pseudo:
            //GameboardData.Colors.TryGetValue(Gameboard.CurrentPlayer.Color);

            //int currentPlayerColorIndex = Gameboard.ColorOrder.FindIndex(c => c == Gameboard.CurrentPlayer.Color);

            //int nextPlayerColorIndex = currentPlayerColorIndex + 1 < OrderPlayers.Count ? currentPlayerColorIndex + 1 : 0;

            //Color nextPlayerColor = Gameboard.ColorOrder.ElementAtOrDefault(currentPlayerColorIndex)


            //var NextPlayer  = Gameboard.Players.SingleOrDefault(p => p.Color == nextPlayerColor);

            //Gameboard.CurrentPlayer = NextPlayer();
            //return NextPlayer;
        //}

        public void StartGame()
        {
            // move inline?
            //Player player = DecideWhoStarts();
            // update database with startdate
            NextTurn();
        }

        public void NextTurn()
        {
            // Get data from database
            throw new NotImplementedException();
        }

        public List<MoveAction> HandleTurn(Gameboard gameboard, Player player)
        {
            int diceNumber = RollDice();
            return _game.GetPossibleMoves(gameboard, player, diceNumber);
        }

        public int RollDice()
        {
            //return _die.Next(1, 7);
            return _die.RollDie();
        }

        public async Task StartGameAsync(Gameboard gameboard)
        {
            var player = DecideWhoStarts(gameboard);
            await _gameRepository.StartGameAsync(_context, gameboard);
            await _gameRepository.SetCurrentPlayer(_context, gameboard, player);
        }

        //public void EndTurn()
        //{
        //    throw new NotImplementedException();
        //}

        //public void HandleTurn()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
