using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Ludo_API.Models;
using Ludo_WebApp.Models;
using Ludo_WebApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ludo_WebApp.Pages.Ludo
{
    public class IndexModel : PageModel
    {
        public LudoData LudoData { get; set; }

        //[BindProperty(SupportsGet = true)]
        public NewPlayerDTO NewPlayer { get; set; }

        //[BindProperty(SupportsGet = true)]
        public GameboardDTO Gameboard { get; set; }

        public string ColorError { get; set; }

        //[BindProperty]
        //public GameboardDTO gameboard { get; set; }

        public async Task OnGet(int? id, GameboardDTO gameboard, LudoData ludoData)
        {
            if (id == null)
            {
                // todo: redirect to error-page / "Index" / "Pages/New"?
                return;
            }

            // Check if this is already stored in a cookie/local storage
            // Read cookie on server and automatically send if not set?
            var restResponseLudoData = await Fetch.GetLudoData();
            if (restResponseLudoData.StatusCode != HttpStatusCode.OK)
            {
                // redirect to error page?
                ModelState.AddModelError("ApiErrorLudoData", "Could not get Ludo data");
            }

            LudoData = restResponseLudoData.Data;

            // Get Gameboard if one exists with this id.
            var restResponse = await Fetch.GetGame(id.Value);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ApiBadRequest", restResponse.Content);
                // todo: redirect to error-page?
                // todo: logging?
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
                // todo: redirect to error-page?
                // todo: logging?
            }

            return RedirectToPage("./Index/", new { id = restResponse.Data.ID, successfullyJoined = 1 });
        }

        public async Task<IActionResult> OnPostStartGameAsync(GameboardDTO gameboard)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
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
            return RedirectToRoute(Request.Path);
        }
    }
}
