using System;
using System.Collections.Generic;

namespace Ludo_WebApp.Ludo_API.Models
{
    static public class MoveMessagesClass
    {
        public const string NoPossibleMoves = "No possible moves";
        public const string MoveSuccessful = "Move successful";
        public const string CantPassYourOwn = "You can't pass or stop on a square you occupy";
        public const string KnockOutOpponent = "You've knocked your opponent's piece(s) out.";
        public const string PieceEnteredGoal = "You've moved a piece to the goal square.";
    }

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
