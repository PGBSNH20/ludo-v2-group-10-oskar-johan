using System;
using System.ComponentModel.DataAnnotations;

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
            if (player == null)
            {
                throw new NullReferenceException("'player' (Player) is null.");
            }

            ID = player.ID;
            Name = player.Name;
            Color = player.Color;
        }
    }
}
