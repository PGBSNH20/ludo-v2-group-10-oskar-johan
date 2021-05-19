using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Ludo_API.GameEngine.Game
{
    public struct Track60Data : ITrackData
    {
        [Required]
        public List<int> Track { get; }

        [Required]
        public int StartIndex { get; }

        [Required]
        public int StartSixthIndex { get; }

        [Required]
        public int GoalIndex { get; }

        public Track60Data(List<int> track)
        {
            Track = track;
            StartIndex = track[0];
            StartSixthIndex = track[5];
            GoalIndex = track[^1];
        }
    }
}
