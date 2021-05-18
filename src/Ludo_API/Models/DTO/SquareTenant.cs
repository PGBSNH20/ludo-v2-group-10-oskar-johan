using Ludo_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Ludo_API
{
    public record SquareTenant
    {
        [Key]
        //public int ID { get; }
        public int ID { get; set; }

        [ForeignKey("Square")]
        //public int SquareIndex { get; }
        public int SquareIndex { get; set; }

        //[ForeignKey("Player")]
        //public Player Player { get; }
        public Player Player { get; set; }

        [Range(0, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //public int PieceCount { get; }
        public int PieceCount { get; set; }

        public SquareTenant()
        {
        }

        public SquareTenant(int squareIndex, Player player, int pieceCount)
        {
            SquareIndex = squareIndex;
            Player = player;
            PieceCount = pieceCount;
        }
    }
}