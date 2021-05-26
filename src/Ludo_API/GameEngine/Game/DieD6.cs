using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class DieD6 : IDie
    {
        private Random _die = new Random();

        public int RollDie()
        {
            return _die.Next(1, 7);
        }
    }
}
