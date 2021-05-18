using Ludo_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Ludo_API
{
    public record SquareTenant
    {
        [ForeignKey("Square")]
        public int SquareIndex { get; }

        [ForeignKey("Player")]
        public Player Player { get; }

        [Range(0, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PieceCount { get; }

        public SquareTenant(int squareIndex, Player player, int pieceCount)
        {
            SquareIndex = squareIndex;
            Player = player;
            PieceCount = pieceCount;
        }
    }
}