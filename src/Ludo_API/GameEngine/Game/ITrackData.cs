using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ludo_API.GameEngine.Game
{
    public interface ITrackData
    {
        List<int> Track { get;  }
        public int StartIndex { get; }
        public int StartSixthIndex { get; }
        int GoalIndex { get;  }
    }
}