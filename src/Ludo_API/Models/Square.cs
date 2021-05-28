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

        [Required]
        public SquareTenant Tenant { get; set; }

        [Required]
        public Gameboard Gameboard { get; set; }

        public int GameboardId { get; set; }
        #endregion

        public Square()
        {
        }
    }
}
