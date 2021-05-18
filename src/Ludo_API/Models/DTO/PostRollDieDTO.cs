using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public class PostRollDieDTO
    {
        [Range(0, int.MaxValue)]
        public int GameId { get; set; }

        [Range(0, int.MaxValue)]
        public int PlayerId { get; set; }
    }
}
