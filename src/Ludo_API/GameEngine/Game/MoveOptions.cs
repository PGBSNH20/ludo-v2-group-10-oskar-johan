using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class MoveOptions
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Action> PossibleMoves { get; set; }
        public MoveOptions(int id, string text, List<Action> possibleMoves)

        {
            Id = id;
            Text = text;
            PossibleMoves = possibleMoves;
        }
    }
}
