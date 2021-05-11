using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ludo_API.Models
{
    public class Square
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public Player OccupiedBy { get; set; }

        public int? PieceCount { get; set; }

        public Gameboard Gameboard { get; set; }

        public int GameboardId { get; set; }

        public Square()
        {
        }
    }
}
