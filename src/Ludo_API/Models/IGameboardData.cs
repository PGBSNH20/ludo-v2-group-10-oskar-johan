using Ludo_API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ludo_API
{
    public interface IGameboardData
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int[] GameboardSquareIndices { get; set; }
        public int[,] GameboardMapIndices { get; set; }
        public string[,] GameboardMapIndices_squarestrings { get; set; }
        public string[] GameboardMapIndices_rowstrings { get; set; }
        public string[,] GameboardMapColors { get; set; }
        public string[] GameboardMapColors_rowstrings { get; set; }
        //public JsonKeyValue Colors { get; set; }
        public Dictionary<string, TrackColorData> Colors { get; set; }
    }
}
