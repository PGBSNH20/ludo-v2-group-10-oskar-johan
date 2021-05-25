using Ludo_WebApp.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ludo_WebApp
{
    public record MoveAction
    {
        //[DataMember] // unnecessary? // (if used as input DTO) make it impossible to set this property on input
        public int Id { get; init; }

        //[DataMember] // unnecessary?
        public int GameId { get; init; } // necessary?
        #region DataMembers

        //[DataMember] // unnecessary?
        public int PlayerId { get; init; } // necessary?

        //[Required]
        //[DataMember] // unnecessary?
        public string OptionText { get; init; }

        //[Required]
        //[DataMember] // unnecessary?
        public string Message { get; init; }

        //[Range(1, 6, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        //[DataMember] // unnecessary?
        public int DiceRoll { get; init; }

        //[Required]
        //[DataMember] // unnecessary? // (if used as input DTO) make it impossible to set this property on input
        public bool ValidMove { get; init; }
        #endregion

        #region NonDataMembers
        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        public SquareTenantDTO StartSquare { get; init; }
        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        //[Required] // note: will this work? will it serialize it even though it's not a [DataMember]? ((if used as input DTO) will it throw/error because it's not set?)
        public SquareTenantDTO DestinationSquare { get; init; }
        #endregion

        //public int SquareIndex { get; set; }
        //public Player NewPlayerValue { get; set; }
        //public int NewPieceCount { get; set; }
        //public object SquaresToUpdate { get; internal set; }
    }
}