using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/json-and-xml-serialization#what-gets-serialized
    [DataContract] // note: does this work for json?
    public record MoveAction
    {
        [Key]
        [DataMember] // unnecessary? // (if used as input DTO) make it impossible to set this property on input
        public int Id { get; init; }

        //[ForeignKey("Gameboard")]
        [DataMember] // unnecessary?
        public int GameId { get; init; } // necessary?
        #region DataMembers

        [ForeignKey("Player")]
        [DataMember] // unnecessary?
        public int PlayerId { get; init; } // necessary?

        [Required]
        [DataMember] // unnecessary?
        public string OptionText { get; init; }

        [Range(1, 6, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DataMember] // unnecessary?
        public int DiceRoll { get; init; }

        [Required]
        [DataMember] // unnecessary? // (if used as input DTO) make it impossible to set this property on input
        public bool ValidMove { get; init; }
        #endregion

        #region NonDataMembers
        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        public SquareTenant StartSquare { get; init; }
        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        [Required] // note: will this work? will it serialize it even though it's not a [DataMember]? ((if used as input DTO) will it throw/error because it's not set?)
        public SquareTenant DestinationSquare { get; init; }
        #endregion

        //public int SquareIndex { get; set; }
        //public Player NewPlayerValue { get; set; }
        //public int NewPieceCount { get; set; }
        //public object SquaresToUpdate { get; internal set; }
    }
}
