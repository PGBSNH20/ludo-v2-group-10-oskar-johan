using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public record PlayerDTO
    {
        public string Name { get; set; }
        // Which datatype is correct?
        //public string Color { get; set; } // e.g. "#ff00ff00" for green.
        public int Color { get; set; } // e.g. 0xff00ff00 for green.
    }
}
