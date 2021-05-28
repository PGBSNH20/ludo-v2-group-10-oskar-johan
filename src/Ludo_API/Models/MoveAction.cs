using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ludo_API.Models
{
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/json-and-xml-serialization#what-gets-serialized
    //[DataContract] // note: does this work for json?
    public class MoveAction
    {
        [Key]
        public int Id { get; init; }

        public int GameId { get; init; } // necessary?
        public Player Player { get; init; } // necessary?

        [ForeignKey("Player")]
        public int PlayerId { get; init; } // necessary?

        [Required]
        public string OptionText { get; init; }

        [Required]
        public string Message { get; init; }

        [Range(1, 6, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int DiceRoll { get; init; }

        [Required]
        public bool ValidMove { get; init; }

        public SquareTenant StartSquare { get; init; }
        public SquareTenant DestinationSquare { get; init; }
    }
}
