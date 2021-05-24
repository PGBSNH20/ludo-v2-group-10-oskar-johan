using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_WebApp.Utils
{
    public static class CookieMonster
    {
        public static void SetCookie(IResponseCookies cookies, string key, string value, int? expireTime = null)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                option.Expires = DateTime.Now.AddYears(1);
            }

            cookies.Append(key, value, option);
        }
    }
}
