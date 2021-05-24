using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Models;
using Ludo_WebApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ludo_WebApp.Pages.Ludo
{
    public class NewModel : PageModel
    {
        [BindProperty]
        public NewPlayerDTO NewPlayer { get; set; }

        //[BindProperty]
        //public string Color { get; set; }
        public Dictionary<string, string> Colors { get; set; } = new()
        {
            { "Yellow", "#ffd700" },
            { "Red", "#ff0000" },
            { "Blue", "#0000ff" },
            { "Green", "#008000" },
        };

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            // call API to create game
            var restResponse = await Fetch.PostNewGameAsync(NewPlayer);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("CreateNewGameError", restResponse.Content);
                return Page();

                // redirect to Ludo/{id}
                //e.g. return RedirectToPage("./Index/{id}");
                //return RedirectToPage("./Index", new { id = response });
            }

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddYears(1);

            string playerId = restResponse.Data.Players.FirstOrDefault().ID.ToString();

            Response.Cookies.Append("PlayerID", playerId, option);

            //return RedirectToPage("./Index", new { id = restResponse.Data });
            return RedirectToPage("./Index", new { id = restResponse.Data.ID });
        }
    }
}
