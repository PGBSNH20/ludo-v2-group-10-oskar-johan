using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ludo_WebApp.Pages.Ludo
{
    enum SquareTypes
    {
        DiceThrowSource = -4, // Corner
        NestSquare = -3,
        None = -1
        // 0-59 = normal/goal-stretch squares
    }

    public class IndexModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        public NewPlayerDTO NewPlayer { get; set; }

        //[BindProperty(SupportsGet = true)]
        public GameboardDTO Gameboard { get; set; }

        public string ColorError { get; set; }

        //[BindProperty]
        //public GameboardDTO gameboard { get; set; }

        //[BindProperty]
        //public string Color { get; set; }

        public Dictionary<string, string> Colors { get; set; } = new()
        {
            { "Yellow", "#cfae00" },
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

        public Dictionary<string, (char squareTypeColorKey, string colorHex)> AllowedColorsData { get; set; } = new()
        {
            //{ 'y', ("yellow", "#ffd700") },
            { "Yellow", ('y', "#cfae00") },
            { "Red", ('r', "#f00") },
            { "Blue", ('b', "#00f") },
            { "Green", ('g', "#008000") },
            { "Goal", ('x', "turquoise") },
            { "Blank", ('b', "#b9b9b9") },
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

        //public async Task<IActionResult> OnGet()
        public async Task OnGet(int? id)
        {
            if (id == null)
            {
                // todo: do something
                //return RedirectToPage("~/Index/");
                return;
            }

            // Get Gameboard if one exists with this id.
            var restResponse = await Fetch.GetGame(id.Value);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ApiBadRequest", restResponse.Content);
                //return Page();
                //return Page();
                //// todo: do something
                //throw new Exception("Fixme");
                //return;
            }

            Gameboard = restResponse.Data;
            //return EmptyResult();
            // todo: update the html
        }

        //[NonHandler]
        public async Task<IActionResult> OnPostAddPlayerAsync(int id, NewPlayerDTO newPlayer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            newPlayer.GameId = id;

            // call API to the add player
            var restResponse = await Fetch.PostAddPlayerAsync(newPlayer);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("Color", restResponse.ErrorMessage);
                return Page();
                // todo: do something
                //ColorError = restResponse.ErrorMessage;
                //return new BadRequestResult();
                //return Page();
                //return RedirectToPage("./Index/", new { id = id });
            }

            // redirect to Ludo/{id}
            //e.g. return RedirectToPage("./Index/{id}");
            return RedirectToPage("./Index/", new { id = restResponse.Data.ID, successfullyJoined = 1 });
        }

        public async Task<IActionResult> OnPostStartGameAsync(GameboardDTO gameboard)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            //// call API to create game
            //var response = ;

            //// redirect to Ludo/{id}
            ////e.g. return RedirectToPage("./Index/{id}");
            //return RedirectToPage("./Index/", new { id = response });

            var restResponse = await Fetch.StartGameAsync(gameboard.ID);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                // todo: do something
                return new BadRequestResult();
            }

            Gameboard = restResponse.Data;
            //return RedirectToRoute(Request.Path.Value, new { id = gameboard.ID });
            var a = Request.Path;
            return RedirectToRoute(Request.Path);
        }
    }
}
