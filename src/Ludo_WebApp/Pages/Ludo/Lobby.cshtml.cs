using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Ludo_API.Models;
using Ludo_WebApp.Models.DTO;
using Ludo_WebApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;

namespace Ludo_WebApp.Pages.Ludo
{
    public class LobbyModel : PageModel
    {
        /* --- Bound Properties ----------------------------- */

        [BindProperty]
        public NewPlayerDTO NewPlayer { get; set; }


        /* --- Properties ----------------------------------- */

        public LudoData LudoData { get; set; }
        public GameboardDTO Gameboard { get; set; }


        /* --- Actions -------------------------------------- */

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                /* --- Get the Gameboard with the {id} ------------- */

                //var restResponseGame = await Fetch.GetAsync<GameboardDTO>(Fetch.RequestURLs.Games, new { id = id.Value });
                var restResponseGame = await Fetch.GetAsync<GameboardDTO>(Fetch.RequestURLs.Games,  id.Value);

                if (restResponseGame.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("Error.Lobby.OnGet.GetGameboardDTO.Debug", restResponseGame.ErrorMessage);
                    ModelState.AddModelError("Error.Lobby.OnGet.GetGameboardDTO", restResponseGame.Content);
                    return Page();
                }

                Gameboard = restResponseGame.Data;
            }

            /* --- Get Ludodata -------------------------------- */

            // Check if this is already stored in a cookie/local storage
            // Read cookie on server and automatically send if not set?
            var restResponseLudoData = await Fetch.GetAsync<LudoData>(Fetch.RequestURLs.GamesLudoData);

            if (restResponseLudoData.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("Error.Lobby.OnGet.GetLudoData.Debug", restResponseLudoData.ErrorMessage);
                ModelState.AddModelError("Error.Lobby.OnGet.GetLudoData", restResponseLudoData.Content);
                return Page();
            }

            LudoData = restResponseLudoData.Data;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            NewPlayer.GameId = id;

            if (NewPlayer.GameId == null)
            {
                // Create a new game with the new player
                var restResponse = await Fetch.PostAsync<GameboardDTO>(Fetch.RequestURLs.GamesNew, NewPlayer);

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("Error.Lobby.OnPost.NewGame.Debug", restResponse.ErrorMessage);
                    ModelState.AddModelError("Error.Lobby.OnPost.NewGame", restResponse.Content);
                    return Page();
                }

                CookieMonster.SetCookie(Response.Cookies, "PlayerID", restResponse.Data.GameCreator.ID.ToString());
                return RedirectToPage("./Lobby", new { id = restResponse.Data.ID });
            }
            else
            {
                // Add the new player to the game
                var restResponse = await Fetch.PostAsync<NewPlayerDTO>(Fetch.RequestURLs.GamesAddPlayer, NewPlayer);

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("Error.Lobby.OnPost.AddPlayer.Debug", restResponse.ErrorMessage);
                    ModelState.AddModelError("Error.Lobby.OnPost.AddPlayer", restResponse.Content);
                    return Page();
                }

                CookieMonster.SetCookie(Response.Cookies, "PlayerID", restResponse.Data.ID.ToString());
                return RedirectToPage("./Lobby", new { id = id });
            }
        }

        public async Task<IActionResult> OnPostStartGameAsync(GameboardDTO gameboard)
        {
            if (gameboard == null || gameboard.ID == null)
            {
                ModelState.AddModelError("Gameboard.ID", "Gameboard or Gameboard.ID is null.");
                return Page();
            }

            var restResponse = await Fetch.StartGameAsync(gameboard.ID);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                // todo: do something
                return new BadRequestResult();
            }

            Gameboard = restResponse.Data;
            //return RedirectToRoute(Request.Path.Value, new { id = gameboard.ID });
            return RedirectToPage("./Index/", new { id = restResponse.Data.ID, gameSuccessfullyStarted = 1 });
        }
    }
}
