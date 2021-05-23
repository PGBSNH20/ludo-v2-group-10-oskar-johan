using Ludo_API.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public record PlayerDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        //[IsColor("Invalid color format, value should be a hex color (e.g. #123cef)")]
        [RegularExpression("^(" + Player.ValidColorsPattern + ")$")]
        public string Color { get; set; } // e.g. "#ff00ff00" for green.

        //public int Color { get; set; } // e.g. 0xff00ff00 for green.

        public PlayerDTO(Player player)
        {
            ID = player.ID;
            Name = player.Name;
            Color = player.Color;
        }
    }
}
