using Ludo_WebApp.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Ludo_WebApp
{
    public record MoveAction
    {
        public int Id { get; init; }
        public int GameId { get; init; }
        public PlayerDTO Player { get; init; }
        public int PlayerId { get; init; }
        public string OptionText { get; init; }
        public string Message { get; init; }
        public int DiceRoll { get; init; }
        public bool ValidMove { get; init; }

        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        public SquareTenantDTO StartSquare { get; init; }

        /// <summary>
        /// This holds the new value for the <see cref="Square"/> a piece is moved from.
        /// </summary>
        public SquareTenantDTO DestinationSquare { get; init; }
    }
}