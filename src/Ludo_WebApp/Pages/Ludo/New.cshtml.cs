using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ludo_WebApp.Pages.Ludo
{
    public class NewModel : PageModel
    {
        [BindProperty]
        public NewGameDTO NewGame { get; set; }

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
                return Page();
            }

            // call API to create game
            var response = await Fetch.PostNewGameAsync(NewGame);

            // redirect to Ludo/{id}
            //e.g. return RedirectToPage("./Index/{id}");
            return RedirectToPage("./Index/", new { id = response });
        }
    }
}
