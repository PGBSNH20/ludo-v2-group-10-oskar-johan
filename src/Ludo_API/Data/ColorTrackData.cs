namespace Ludo_API.Data
{
    public class ColorTrackData
    {
        public int[] TrackIndices { get; }
        public int StartIndex { get; }
        public int GoalIndex { get; }
        public string ColorHex { get; }
        public char ColorMapKey { get; }

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
        }
    }
}
