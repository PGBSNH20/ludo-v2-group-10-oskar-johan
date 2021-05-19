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
    enum SquareTypes {
        DiceThrowSource = -4, // Corner
        NestSquare = -3,
        None = -1
        // 0-59 = normal/goal-stretch squares
    }

    public class IndexModel : PageModel
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

        //    [BindProperty(SupportsGet = true)]
        //    public GameboardConfigORM gameboardConfig { get; set; }

        // fixme: this is hacky
        public int ColumnCount { get; set; } = 11; // get this value from the API?
        public int RowCount { get; set; } = 11; // get this value from the API?

        // fixme: this is hacky
        public int[,] SquareIndices { get; set; } = new int[11, 11]
        {
            {-4,-1,-1,-1,18,19,20,-1,-1,-1,-4},
            {-1,-3,-3,-1,17,45,21,-1,-3,-3,-1},
            {-1,-3,-3,-1,16,46,22,-1,-3,-3,-1},
            {-1,-1,-1,-1,15,47,23,-1,-1,-1,-1},
            {10,11,12,13,14,48,24,25,26,27,28},
            { 9,40,41,42,43,-2,53,52,51,50,29},
            { 8, 7, 6, 5, 4,58,34,33,32,31,30},
            {-1,-1,-1,-1, 3,57,35,-1,-1,-1,-1},
            {-1,-3,-3,-1, 2,56,36,-1,-3,-3,-1},
            {-1,-3,-3,-1, 1,55,37,-1,-3,-3,-1},
            {-4,-1,-1,-1, 0,39,38,-1,-1,-1,-4},
        };

        // fixme: this is hacky
        public Dictionary<char, (string colorName, string colorHex)> Colors2 { get; set; } = new()
        {
            //{ 'y', ("yellow", "#ffd700") },
            { 'y', ("yellow", "#cfae00") },
            { 'r', ("red", "#f00") },
            { 'b', ("blue", "#00f") },
            { 'g', ("green", "#008000") },
            { 'x', ("goal", "turquoise") },
            { ' ', ("blank", "#b9b9b9") },
        };

        // fixme: this is hacky
        public char[,] SquareColors { get; set; } = new char[11, 11]
        {
            {'r',' ',' ',' ',' ',' ','b',' ',' ',' ','b'},
            {' ','r','r',' ',' ','b',' ',' ','b','b',' '},
            {' ','r','r',' ',' ','b',' ',' ','b','b',' '},
            {' ',' ',' ',' ',' ','b',' ',' ',' ',' ',' '},
            {'r',' ',' ',' ',' ','b',' ',' ',' ',' ',' '},
            {' ','r','r','r','r','x','g','g','g','g',' '},
            {' ',' ',' ',' ',' ','y',' ',' ',' ',' ','g'},
            {' ',' ',' ',' ',' ','y',' ',' ',' ',' ',' '},
            {' ','y','y',' ',' ','y',' ',' ','g','g',' '},
            {' ','y','y',' ',' ','y',' ',' ','g','g',' '},
            {'y',' ',' ',' ','y',' ',' ',' ',' ',' ','g'},
        };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAddPlayerAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            // call API to create game
            var response = await Fetch.PostAddPlayerAsync(NewPlayer);

            // redirect to Ludo/{id}
            //e.g. return RedirectToPage("./Index/{id}");
            return RedirectToPage("./Index/", new { id = response });
        }
    }
}
