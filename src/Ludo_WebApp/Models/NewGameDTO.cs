using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Models
{
    public class NewGameDTO
    {
        //public string GameName { get; set; } // todo: if time permits
        [Range(1, 4)]
        public int PlayerCount { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string FirstPlayerName { get; set; }

        [Required]
        [RegularExpression("#[0-9a-fA-F]{6}")]
        public string FirstPlayerColor { get; set; }
    }
}
