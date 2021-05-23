using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ludo_WebApp.Models.DTO
{
    public record SquareTenantDTO
    {
        public int ID { get; set; }
        public int SquareIndex { get; set; }
        public PlayerDTO Player { get; set; }
        public int PieceCount { get; set; }

        public SquareTenantDTO()
        {
        }
    }
}
