using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Data
{
    public class SimpleResponse<T>
    {
        public T Data { get; set; }

        public SimpleResponse(T value)
        {
            Data = value;
        }
    }
}
