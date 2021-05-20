using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public class GameboardDTO
    {
        public Player CurrentPlayer { get; set; }
        public List<Square> Squares { get; set; }
        public List<Player> Players { get; set; }
        public DateTime? GameDate  { get; set; } // todo: rename to something like "lastturndate"
        public DateTime? GameStartDate { get; set; }
        //GameboardData GameboardData { get; set; } // Add ColorOrder?

        public GameboardDTO(Gameboard gameboard)
        {
            CurrentPlayer = gameboard.CurrentPlayer;
            Squares = gameboard.Squares;
            Players = gameboard.Players;
            GameDate = gameboard.GameDate;
            GameStartDate = gameboard.GameStartDate;
        }
    }
}

