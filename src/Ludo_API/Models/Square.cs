using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Ludo_API.Models
{
    public class Square
    {
        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; } // todo: rename this to Index? or add new "Index" column?

        public SquareTenant Tenant { get; set; }

        //public Player OccupiedBy { get; set; }

        // note: why is this nullable?
        //public int? PieceCount { get; set; }

        [Required]
        public Gameboard Gameboard { get; set; }

        // note: do we need this?
        public int GameboardId { get; set; }
        #endregion

        public Square()
        {
        }
    }
}
