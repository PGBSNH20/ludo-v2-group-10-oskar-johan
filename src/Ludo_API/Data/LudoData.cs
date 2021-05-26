using System;
using System.Collections.Generic;

namespace Ludo_API.Data
{

    public sealed class LudoData
    {
        #region Singleton
        private static readonly Lazy<LudoData> lazy = new(() => new LudoData());
        public static LudoData Instance { get { return lazy.Value; } }
        #endregion

        #region Data
        public int GameboardRowCount { get; } = 11;
        public int GameboardColumnCount { get; } = 11;

        //// disable code formatting: https://github.com/dotnet/roslyn/issues/36930#issuecomment-530441099
        //#pragma warning disable format
        public int[,] GameboardMapIndices { get; } = new[,] {
            {-1,-1,-1,-1,18,19,20,-1,-1,-1,-1,},
            {-1,-3,-3,-1,17,45,21,-1,-3,-3,-1,},
            {-1,-3,-3,-1,16,46,22,-1,-3,-3,-1,},
            {-1,-1,-1,-1,15,47,23,-1,-1,-1,-1,},
            {10,11,12,13,14,48,24,25,26,27,28,},
            { 9,40,41,42,43,-2,53,52,51,50,29,},
            { 8, 7, 6, 5, 4,58,34,33,32,31,30,},
            {-1,-1,-1,-1, 3,57,35,-1,-1,-1,-1,},
            {-1,-3,-3,-1, 2,56,36,-1,-3,-3,-1,},
            {-1,-3,-3,-1, 1,55,37,-1,-3,-3,-1,},
            {-1,-1,-1,-1, 0,39,38,-1,-1,-1,-1 },
        };

        public char[,] GameboardMapColors { get; } = new[,] {
           { ' ',' ',' ',' ',' ',' ','b',' ',' ',' ',' ' },
           { ' ','r','r',' ',' ','b',' ',' ','b','b',' ' },
           { ' ','r','r',' ',' ','b',' ',' ','b','b',' ' },
           { ' ',' ',' ',' ','r','b',' ',' ',' ',' ',' ' },
           { 'r',' ',' ',' ',' ','b',' ','b',' ',' ',' ' },
           { ' ','r','r','r','r','x','g','g','g','g',' ' },
           { ' ',' ',' ','y',' ','y',' ',' ',' ',' ','g' },
           { ' ',' ',' ',' ',' ','y','g',' ',' ',' ',' ' },
           { ' ','y','y',' ',' ','y',' ',' ','g','g',' ' },
           { ' ','y','y',' ',' ','y',' ',' ','g','g',' ' },
           { ' ',' ',' ',' ','y',' ',' ',' ',' ',' ',' ' },
        };
        //#pragma warning restore format

        public Dictionary<char, string> ColorKeyMap { get; } = new()
        {
            { 'y', "Yellow" },
            { 'r', "Red" },
            { 'b', "Blue" },
            { 'g', "Green" },
        };

        public Dictionary<string, ColorTrackData> ColorTracks { get; } = new()
        {
            { "Yellow", null },
            { "Red", null },
            { "Blue", null },
            { "Green", null },
        };
        #endregion

        #region Static Getters
        public static int GetGameboardRowCount { get => Instance.GameboardRowCount; }
        public static int GetGameboardColumnCount { get => Instance.GameboardColumnCount; }
        public static int[,] GetGameboardMapIndices { get => Instance.GameboardMapIndices; }
        //public static string[] GetGameboardMapColors { get => Instance.GameboardMapColors; }
        public static char[,] GetGameboardMapColors { get => Instance.GameboardMapColors; }
        public Dictionary<string, ColorTrackData> GetColorTracks { get => Instance.ColorTracks; }
        #endregion

        private LudoData()
        {
            int[] YellowTrackIndices = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 };
            int[] RedTrackIndices = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 45, 46, 47, 48, 49 };
            int[] BlueTrackIndices = new[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 50, 51, 52, 53, 54 };
            int[] GreenTrackIndices = new[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 55, 56, 57, 58, 59 };

            ColorTracks["Yellow"] = new ColorTrackData(YellowTrackIndices, "#ffd700", 'y');
            ColorTracks["Red"] = new ColorTrackData(RedTrackIndices, "#ff0000", 'r');
            ColorTracks["Blue"] = new ColorTrackData(BlueTrackIndices, "#0000ff", 'b');
            ColorTracks["Green"] = new ColorTrackData(GreenTrackIndices, "#008000", 'g');
        }
    }
}
