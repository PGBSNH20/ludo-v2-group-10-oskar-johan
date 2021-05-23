using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Models.DTO
{
    public class GameboardDTO
    {
        public int ID { get; set; }
        [DisplayName("Current Player")]
        public PlayerDTO CurrentPlayer { get; set; }
        public List<SquareDTO> Squares { get; set; }
        public List<PlayerDTO> Players { get; set; }
        [DisplayName("Game Date")]
        public DateTime? GameDate { get; set; } // todo: rename to something like "lastturndate"
        [DisplayName("Game Start Date")]
        public DateTime? GameStartDate { get; set; }
        public PlayerDTO GameCreator { get; set; }
        //GameboardData GameboardData { get; set; } // Add ColorOrder?

        public GameboardDTO()
        {
        }
    }
}
