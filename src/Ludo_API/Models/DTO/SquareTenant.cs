using Ludo_API.Models;

namespace Ludo_API
{
    public record SquareTenant
    {
        public int SquareIndex { get; }
        public Player Player { get; }
        public int PieceCount { get; }

        public SquareTenant(int squareIndex, Player player, int pieceCount)
        {
            SquareIndex = squareIndex;
            Player = player;
            PieceCount = pieceCount;
        }
    }
}