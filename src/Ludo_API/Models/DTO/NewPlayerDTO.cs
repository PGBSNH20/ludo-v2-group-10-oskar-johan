﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public class NewPlayerDTO
    {
        public int? ID { get; set; }

        public int? GameId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string PlayerName { get; set; }

        [Required]
        [RegularExpression("^(" + Player.ValidColorsPattern + ")$")]
        public string PlayerColor { get; set; }
    }
}
