using System;
using System.Collections.Generic;

namespace Ludo_WebApp.Ludo_API.Models
{
    public class ColorTrackData
    {
        public List<int> TrackIndices { get; set; }
        public int StartIndex { get; set; }
        public int GoalIndex { get; set; }
        public string ColorHex { get; set; }
        public char ColorMapKey { get; set; }

        public ColorTrackData()
        {
        }
    }

    public class LudoData
    {
        public int GameboardRowCount { get; set; }
        public int GameboardColumnCount { get; set; }
        public List<List<int>> GameboardMapIndices { get; set; }
        public List<List<char>> GameboardMapColors { get; set; }
        public Dictionary<char, string> ColorKeyMap { get; set; }
        public Dictionary<string, ColorTrackData> ColorTracks { get; set; }

        public LudoData()
        {
        }
    }
}
