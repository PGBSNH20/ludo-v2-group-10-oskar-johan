using Ludo_API.Models;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{

    //internal class TurnManager : ITurnBased
    internal class TurnManager
    {
        private readonly IGamesRepository _gameRepository;
        private readonly Random _die;
        private readonly Game _game;

        public TurnManager(Game game)
        {
            _game = game;
            _die = new Random();
        }

        #region ITurnBased
        public Models.Player DecideWhoStarts(List<Models.Player> players)
        {
            var startnumber = new Random();
            var start = startnumber.Next(0, players.Count);
            return players[start];
        }

        public void NextTurn()
        {
            throw new NotImplementedException();
        }

        public void HandleTurn(Models.Player player)
        {
            //Console.WriteLine($"\nDet är {player.Name}s tur att slå.");
            //int choice = Menu.ShowMenu("Gör ditt val i listan\n", new string[]
            //{
            //    "Slå tärningen",
            //    "Avsluta"
            //});

            // Send message to client with options?
            int choice = 0; // note: temporary variable.

            switch (choice)
            {
                case 0:
                    int diceNumber;
                    do
                    {
                        diceNumber = RollDice();
                        Console.WriteLine($"{player.Name} slog {diceNumber}");
                        _game.GetPossibleMoves(player, diceNumber);
                        //new Moves(_gameRepository, this).GameMove(_game.Gameboard, _game.Gameboard.Squares, diceNumber, player);
                    } while (diceNumber == 6);

                    _game.Gameboard.LastPlayer = player;
                    _gameRepository.SaveTurnAsync(_game.Gameboard, player);
                    break;
                case 1:
                    //Console.WriteLine("\nTack för idag!");
                    // Send message to client?
                    //EndGame();
                    break;
                default:
                    //Console.WriteLine("Något gick fel!");
                    // Send message to client?
                    //EndGame();
                    break;
            }
        }

        private int RollDice()
        {
            return _die.Next(1, 7);
        }

        public void EndTurn()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
