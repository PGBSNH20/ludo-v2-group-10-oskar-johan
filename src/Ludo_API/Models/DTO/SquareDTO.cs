using System;

namespace Ludo_API.Models.DTO
{
    public class SquareDTO
    {
        public int ID { get; set; }
        public SquareTenantDTO Tenant { get; set; }
        public int GameboardId { get; set; }

        public SquareDTO()
        {
        }

        public SquareDTO(Square square)
        {
            if (square == null)
            {
                throw new NullReferenceException("'square' (Square) is null.");
            }

            ID = square.ID;
            Tenant = square.Tenant == null ? null : new SquareTenantDTO(square.Tenant);
            GameboardId = square.GameboardId;
        }
    }
}