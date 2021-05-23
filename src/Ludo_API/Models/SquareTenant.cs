using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ludo_API.Models
{
    public record SquareTenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Square")]
        public int SquareIndex { get; set; }

        public Player Player { get; set; }

        [Range(0, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
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