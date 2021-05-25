namespace Ludo_API.Data
{
    public class ColorTrackData
    {
        public int[] TrackIndices { get; set; }
        public int StartIndex { get; set; }
        public int GoalIndex { get; set; }
        public string ColorHex { get; set; }
        public char ColorMapKey { get; set; }

        public ColorTrackData(int[] trackIndices, string colorHex, char colorMapKey)
        {
            TrackIndices = trackIndices;
            ColorHex = colorHex;
            ColorMapKey = colorMapKey;

            StartIndex = trackIndices[0];
            GoalIndex = trackIndices[^1];
        }

        public int this[int index]
        {
            get => TrackIndices[index];
            set => TrackIndices[index] = value;
        }
    }
}
