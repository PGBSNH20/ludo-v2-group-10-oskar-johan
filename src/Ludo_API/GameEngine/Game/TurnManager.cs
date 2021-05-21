using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{

    public class TurnManager : ITurnBased
    //internal class TurnManager
    {
        private readonly IGamesRepository _gameRepository;
        private readonly Random _die;
        private readonly Game _game;

        //public TurnManager()
        //{
        //}

        public TurnManager(Game game)
        {
            _game = game;
            _die = new Random();
        }

        #region ITurnBased
        public Player DecideWhoStarts(List<Player> players)
        {
            var startnumber = new Random();
            var start = startnumber.Next(0, players.Count);
            return players[start];
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

        public List<MoveAction> HandleTurn(Player player)
        {
            int diceNumber = RollDice();
            return _game.GetPossibleMoves(player, diceNumber);
        }

        public int RollDice()
        {
            return _die.Next(1, 7);
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
