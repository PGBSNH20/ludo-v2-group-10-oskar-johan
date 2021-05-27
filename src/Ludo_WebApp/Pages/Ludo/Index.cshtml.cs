using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Ludo_API.Models;
using Ludo_WebApp.Models.DTO;
using Ludo_WebApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ludo_WebApp.Pages.Ludo
{
    public class IndexModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        public LudoData LudoData { get; set; }

        ////[BindProperty(SupportsGet = true)]
        //public NewPlayerDTO NewPlayer { get; set; }

        //[BindProperty(SupportsGet = true)]
        public GameboardDTO Gameboard { get; set; }

        //[BindProperty]
        public List<MoveAction> MoveActions { get; set; }

        //[BindProperty]
        public int? ChosenMoveActionId { get; set; }

        //public string ColorError { get; set; }

        //[BindProperty]
        //public GameboardDTO gameboard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameboard"></param>
        /// <param name="ludoData"></param>
        /// <returns></returns>
        //public async Task OnGet(int? id, GameboardDTO gameboard, LudoData ludoData)
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                // todo: redirect to error-page / "Index" / "Pages/New"?
                ModelState.AddModelError("NoId", "You have to specify a gameId");
                return Page();
            }

            /* -------------------------------------------------- */

            // Get Gameboard if one exists with this id.
            var restResponse = await Fetch.GetGame(id.Value);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ApiBadRequest", restResponse.Content);
                // todo: redirect to error-page?
                // todo: logging?
            }

            Gameboard = restResponse.Data;

            // If the game has not started, redirect to the lobby.
            if (Gameboard.GameStartDate == null)
            {
                return RedirectToPage("./Lobby/", new { id = Gameboard.ID });
            }

            //return EmptyResult();
            // todo: update the html

            /* -------------------------------------------------- */

            // Check if this is already stored in a cookie/local storage
            // Read cookie on server and automatically send if not set?
            var restResponseLudoData = await Fetch.GetLudoData();
            if (restResponseLudoData.StatusCode != HttpStatusCode.OK)
            {
                // redirect to error page?
                ModelState.AddModelError("ApiErrorLudoData", "Could not get Ludo data");
            }

            LudoData = restResponseLudoData.Data;

            /* -------------------------------------------------- */

            // Get possible MoveActions
            var restResponseMoveActions = await Fetch.GetAsync<List<MoveAction>>(Fetch.RequestURLs.GameplayGetMoveActions, new PostRollDieDTO(Gameboard));

            if (restResponseMoveActions.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ErrorGetMoveActions", restResponseMoveActions.Content);
                // todo: redirect to error-page?
                // todo: logging?
                MoveActions = null;
                return Page();
            }

            MoveActions = restResponseMoveActions.Data.Count > 0 ? restResponseMoveActions.Data : null;
            return Page();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="newPlayer"></param>
        ///// <returns></returns>
        ////[NonHandler]
        //public async Task<IActionResult> OnPostAddPlayerAsync(int id, NewPlayerDTO newPlayer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "ModelState invalid");
        //        return Page();
        //    }

        //    newPlayer.GameId = id;

        //    // call API to the add player
        //    var restResponse = await Fetch.PostAddPlayerAsync(newPlayer);

        //    if (restResponse.StatusCode != HttpStatusCode.OK)
        //    {
        //        //ModelState.AddModelError("AddPlayer", restResponse.ErrorMessage);

        //        if (restResponse.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            ModelState.AddModelError("AddPlayerBadRequest", restResponse.Content);
        //        }

        //        if (restResponse.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            ModelState.AddModelError("AddPlayerNotFound", restResponse.Content);
        //        }

        //        //return Page();
        //        return Redirect(Request.Path);
        //        // todo: redirect to error-page?
        //        // todo: logging?
        //    }

        //    CookieMonster.SetCookie(Response.Cookies, "PlayerID", restResponse.Data.ID.ToString());

        //    return RedirectToPage("./Index/", new { id = restResponse.Data.GameId, successfullyJoined = 1 });
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="gameboard"></param>
        ///// <returns></returns>
        //public async Task<IActionResult> OnPostStartGameAsync(GameboardDTO gameboard)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "ModelState invalid");
        //        return Page();
        //    }

        //    var restResponse = await Fetch.StartGameAsync(gameboard.ID);

        //    if (restResponse.StatusCode != HttpStatusCode.OK)
        //    {
        //        // todo: do something
        //        return new BadRequestResult();
        //    }

        //    Gameboard = restResponse.Data;
        //    //return RedirectToRoute(Request.Path.Value, new { id = gameboard.ID });
        //    return RedirectToPage("./Index/", new { id = restResponse.Data.ID, gameSuccessfullyStarted = 1 });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostRollDieAsync(GameboardDTO gameboard)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ModelState invalid");
                return Page();
            }

            if (gameboard.CurrentPlayer == null)
            {
                //todo: error handling
                ModelState.AddModelError("PostRollDie_GameboardCurrentPlayer", "Gameboard.CurrentPlayer is null");
                return Page();
            }

            var restResponse = await Fetch.PostAsync<List<MoveAction>>(Fetch.RequestURLs.GameplayRollDie, new PostRollDieDTO(gameboard));

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                // todo: do something
                return new BadRequestResult();
            }

            //MoveActions = restResponse.Data;
            //return RedirectToRoute(Request.Path.Value, new { id = gameboard.ID });
            return RedirectToPage("./Index/", new { id = gameboard.ID, rollDie = true });
            //return Page();
        }

        public async Task<IActionResult> OnPostChooseMoveActionAsync()
        {
            int? chosenId = ChosenMoveActionId;

            var restResponse = await Fetch.PostAsync<List<MoveAction>>(Fetch.RequestURLs.GameplayChoseAction, chosenId);

            return RedirectToPage("./Index/", new { id = Gameboard.ID });
        }
    }
}
