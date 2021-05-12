using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public struct MoveAction
    {
        public int SquareIndex { get; set; }
        public string OptionText { get; set; }
        public Player NewPlayerValue { get; set; }
        public int NewPieceCount { get; set; }
    }
}
