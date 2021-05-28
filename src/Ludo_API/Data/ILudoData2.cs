using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Data
{
    public interface ILudoData2
    {
        public Dictionary<string, List<int>> Tracks { get; }
    }
}
