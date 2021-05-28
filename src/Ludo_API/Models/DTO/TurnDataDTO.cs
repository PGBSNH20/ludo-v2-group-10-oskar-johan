using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public class TurnDataDTO
    {
        public int? DieRoll { get; set; } = null;
        public string Message { get; set; }
        public List<MoveAction> MoveActions { get; set; }
    }
}
