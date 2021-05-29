using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ludo_API.Models.DTO
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

        public SquareTenantDTO(int squareIndex, Player player, int pieceCount)
        {
            SquareIndex = squareIndex;
            Player = new PlayerDTO(player);
            PieceCount = pieceCount;
        }

        public SquareTenantDTO(SquareTenant squareTenant)
        {
            if (squareTenant == null)
            {
                throw new NullReferenceException("'squareTenant' (SquareTenant) is null.");
            }

            ID = squareTenant.ID;
            SquareIndex = squareTenant.SquareIndex;
            Player = squareTenant.Player == null ? null : new PlayerDTO(squareTenant.Player);
            PieceCount = squareTenant.PieceCount;
        }
    }
}