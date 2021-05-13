using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Utils
{
    // Based on: https://stackoverflow.com/a/40917033
    public static class ShortGuid
    {
        public static string CreateShortGuid()
        {
            string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            // Replace URL unfriendly characters and remove trailing ==
            return base64Guid.Replace('+', '-').Replace('/', '_')[..^2];
        }

        public static Guid FromShortGuid(string @string)
        {
            @string = @string.Replace('_', '/').Replace('-', '+');
            return new Guid(Convert.FromBase64String(@string + "=="));
        }
    }
}
