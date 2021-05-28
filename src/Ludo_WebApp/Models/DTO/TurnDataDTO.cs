using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Models.DTO
{
    public class TurnDataDTO
    {
        public int? DieRoll { get; set; }
        public string Message { get; set; }
        public List<MoveAction> MoveActions { get; set; }
    }
}
