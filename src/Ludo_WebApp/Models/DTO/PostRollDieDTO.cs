using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Models.DTO
{
    public class PostRollDieDTO
    {
        [Range(0, int.MaxValue)]
        public int GameId { get; set; }

        // note: Is this still necessary if the gameboard has a CurrentPlayer property?
        [Range(0, int.MaxValue)]
        public int PlayerId { get; set; }

        public PostRollDieDTO(GameboardDTO gameboardDTO)
        {
            GameId = gameboardDTO.ID;
            PlayerId = gameboardDTO.CurrentPlayer.ID;
        }
    }
}
