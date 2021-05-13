using Ludo_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    [DataContract] // note: does this work for json?
    public record MoveAction
    {
        [Key]
        [DataMember] // (if used as input DTO) make it impossible to set this property on input
        public int Id { get; init; }

        [Required]
        [DataMember]
        public int GameId { get; init; } // necessary?
        #region DataMembers

        [Required]
        [DataMember]
        public int PlayerId { get; init; } // necessary?

        [Required]
        [DataMember]
        public string OptionText { get; init; }

        [Required]
        [DataMember] // (if used as input DTO) make it impossible to set this property on input
        public bool ValidMove { get; init; }
        #endregion

        #region NonDataMembers
        [Required] // note: will this work? will it serialize it even though it's not a [DataMember]? ((if used as input DTO) will it throw/error because it's not set?)
        public SquareTenant StartSquare { get; init; }
        public SquareTenant DestinationSquare { get; init; }
        #endregion

        //public int SquareIndex { get; set; }
        //public Player NewPlayerValue { get; set; }
        //public int NewPieceCount { get; set; }
        //public object SquaresToUpdate { get; internal set; }
    }
}
