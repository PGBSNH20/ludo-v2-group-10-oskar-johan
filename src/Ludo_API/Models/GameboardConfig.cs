using Ludo_API.GameEngine.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;

namespace Ludo_API.Models
{
    public class GameboardConfig
    {
        public List<Player> OrderPlayers { get; set; }

        public static List<int> GameboardSquareIndices { get; } = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };

        // This defines which squares makes up the yellow player's track and their order.
        public static List<int> YellowTrack { get; } = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 };
        public static int YellowStartIndex { get; } = YellowTrack[0];
        public static int YellowGoalIndex { get; } = YellowTrack[^1];

        // This defines which squares makes up the red player's track and their order.
        public static List<int> RedTrack { get; } = new() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 45, 46, 47, 48, 49 };
        public static int RedStartIndex { get; } = RedTrack[0];
        public static int RedGoalIndex { get; } = RedTrack[^1];

        // This defines which squares makes up the blue player's track and their order.
        public static List<int> BlueTrack { get; } = new() { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 50, 51, 52, 53, 54 };
        public static int BlueStartIndex { get; } = BlueTrack[0];
        public static int BlueGoalIndex { get; } = BlueTrack[^1];

        // This defines which squares makes up the green player's track and their order.
        public static List<int> GreenTrack { get; } = new() { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 55, 56, 57, 58, 59 };
        public static int GreenStartIndex { get; } = GreenTrack[0];
        public static int GreenGoalIndex { get; } = GreenTrack[^1];



        /// <summary>
        /// <para name="charKey">Key for the color for use against the `SquareColors` 2D array.</para>
        /// Example use:
        /// <code>SquareColors[y, x] == Colors["Yellow"].charKey</code>
        /// </summary>
        public static Dictionary<string, (char charKey, Color color)> ColorValues2 = new()
        {
            { "Yellow", ('y', Color.Gold) },
            { "Red", ('r', Color.Red) },
            { "Blue", ('b', Color.Blue) },
            { "Green", ('g', Color.Green) },
        };
        public Dictionary<string, string> Colors4 { get; set; } = new()
        {
            { "Yellow", "#ffd700" },
            { "Red", "#ff0000" },
            { "Blue", "#0000ff" },
            { "Green", "#008000" },
        };
        public Dictionary<string, ITrackData> Colors3 { get; set; } = new()
        {
            { "Yellow", new Track60Data(YellowTrack) },
            { "Red", new Track60Data(RedTrack) },
            { "Blue", new Track60Data(BlueTrack) },
            { "Green", new Track60Data(GreenTrack) },
        };
        public Dictionary<string, PlayerData> Colors3_1 { get; set; } = new()
        {
            { "Yellow", new PlayerData(YellowTrack, ColorValues2["Yellow"]) },
            { "Red", new PlayerData(RedTrack, ColorValues2["Yellow"]) },
            { "Blue", new PlayerData(BlueTrack, ColorValues2["Yellow"]) },
            { "Green", new PlayerData(GreenTrack, ColorValues2["Yellow"]) },
        };
        public Dictionary<eColors, ITrackData> PlayerData { get; set; } = new()
        {
            { eColors.Yellow, new Track60Data(YellowTrack) },
            { eColors.Red, new Track60Data(RedTrack) },
            { eColors.Blue, new Track60Data(BlueTrack) },
            { eColors.Green, new Track60Data(GreenTrack) },
        };
        public List<Color> ColorOrder = new() { Color.Gold, Color.Red, Color.Blue, Color.Green };

        // following 2 are expriments only
        public enum eColors
        {
            Yellow,
            Red,
            Blue,
            Green
        };
        public Dictionary<eColors, (char charKey, Color color)> ColorValues = new()
        {
            { eColors.Yellow, ('y', Color.Gold) },
            { eColors.Red, ('r', Color.Red) },
            { eColors.Blue, ('b', Color.Blue) },
            { eColors.Green, ('g', Color.Green) },
        };
    }

    public class PlayerData
    {
        public Color Color { get; }
        public List<int> Track { get; }
        public int StartIndex { get; }
        public int StartSixthIndex { get; }
        public int GoalIndex { get; }

        public PlayerData(List<int> track, (char charKey, Color color) colorValues)
        {
            Track = track;
            StartIndex = track[0];
            StartSixthIndex = track[5];
            GoalIndex = track[^1];
        }
    }

    public class TrackColorData
    {
        public Color Color { get; }
        public string ColorHex { get; }
        public char ColorMapKey { get; }
        public List<int> Track { get; }
        public int StartIndex { get; }
        public int StartSixthIndex { get; }
        public int GoalIndex { get; }

        public TrackColorData(List<int> track, (char charKey, Color color) colorValues)
        {
            Track = track;
            StartIndex = track[0];
            StartSixthIndex = track[5];
            GoalIndex = track[^1];
        }
    }

    //public class GameboardData : IGameboardData
    public class GameboardData
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int[] GameboardSquareIndices { get; set; }
        //public List<List<string>> GameboardSquareIndices { get; set; }
        public int[,] GameboardMapIndices { get; set; }
        public string[,] GameboardMapIndices_squarestrings { get; set; }
        public string[] GameboardMapIndices_rowstrings { get; set; }
        //[JsonConverter(typeof(StringArray1dTo2dConverter))]
        //public List<List<string>> GameboardMapIndices_rowstrings { get; set; }
        public string[,] GameboardMapColors { get; set; }
        //[JsonConverter(typeof(StringArray1dTo2dConverter))]
        //public List<List<string>> GameboardMapColors_rowstrings { get; set; }
        public string[] GameboardMapColors_rowstrings { get; set; }
        //public JsonKeyValue Colors { get; set; }
        public Dictionary<string, TrackColorData> Colors { get; set; }
    }

    public class JsonKeyValue
    {
        [JsonExtensionData]
        public Dictionary<string, object> KeyValuePair { get; set; }
    }

    public class GameboardDataORM
    {
        public GameboardDataORM()
        {
            LoadDataAsync();
        }

        static public async Task LoadDataAsync()
        {
            string serializedJsonData;
            try
            {
                var a = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var b = Path.Combine(a, @"Data\GameboardData.json");
                serializedJsonData = await File.ReadAllTextAsync(b);
                var jsonData = JsonConvert.DeserializeObject<GameboardData>(serializedJsonData);
                //var jsonData = JsonConvert.DeserializeObject<GameboardData>(serializedJsonData, new JsonSerializerSettings
                //{
                //    Converters = 
                //});
                var c1 = jsonData.GameboardMapIndices_rowstrings[10].Split(",")[10];
                //var c1 = jsonData.GameboardMapIndices_squarestrings[10, 10];
                var c2 = jsonData.GameboardMapIndices_squarestrings[10, 10];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            //var jsonData = await JsonSerializer.DeserializeAsync(serializedJsonData, typeof(string, object));
            //JavaScriptSerializer oJS = new JavaScriptSerializer();
            //RootObject oRootObject = new RootObject();
            //oRootObject = oJS.Deserialize<RootObject>(Your JSon String);
            //var jsonData = JsonConvert.DeserializeObject<JsonKeyValue>(serializedJsonData);
            //var rootKeys = jsonData.KeyValuePair.Keys;
        }
    }
}
