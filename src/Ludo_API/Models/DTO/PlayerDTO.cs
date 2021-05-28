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
        [RegularExpression("^(" + Player.ValidColorsPattern + ")$")]
        public string Color { get; set; }

        public PlayerDTO(Player player)
        {
            ID = player.ID;
            Name = player.Name;
            Color = player.Color;
        }
    }
}
