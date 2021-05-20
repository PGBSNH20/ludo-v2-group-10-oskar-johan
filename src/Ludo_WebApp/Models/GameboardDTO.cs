using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Models
{
    public class GameboardDTO
    {
        public PlayerDTO CurrentPlayer { get; set; }
        public List<SquareDTO> Squares { get; set; }
        public List<PlayerDTO> Players { get; set; }
        public DateTime GameDate { get; set; } // todo: rename to something like "lastturndate"
        public DateTime GameStartDate { get; set; }
        //GameboardData GameboardData { get; set; } // Add ColorOrder?
    }
}
