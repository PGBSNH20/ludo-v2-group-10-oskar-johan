using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Ludo_API.Models;
using Ludo_WebApp.Models.DTO;
using Ludo_WebApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ludo_WebApp.Pages.Ludo
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public GameboardDTO Gameboard { get; set; }

        public int? DieRoll { get; set; } = null;
        public LudoData LudoData { get; set; }
        public List<MoveAction> MoveActions { get; set; }
        public int? ChosenMoveActionId { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                // todo: redirect to error-page / "Index" / "Pages/New"?
                ModelState.AddModelError("NoId", "You have to specify a gameId");
                return Page();
            }

            if (Request.Query.TryGetValue("DieRoll", out var dieRollString) && int.TryParse(dieRollString, out int dieRoll))
            {
                DieRoll = dieRoll;
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
            //var restResponseMoveActions = await Fetch.GetAsync<TurnResultDTO>(Fetch.RequestURLs.GameplayGetMoveActions, new PostRollDieDTO(Gameboard));

            if (restResponseMoveActions.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ErrorGetMoveActions", restResponseMoveActions.Content);
                // todo: redirect to error-page?
                // todo: logging?
                MoveActions = null;
                return Page();
            }

            MoveActions = restResponseMoveActions.Data.Count > 0 ? restResponseMoveActions.Data : null;

            if (DieRoll == null)
            {
                DieRoll = MoveActions?.FirstOrDefault()?.DiceRoll;
            }

            return Page();
        }

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

            var restResponse = await Fetch.PostAsync<TurnDataDTO>(Fetch.RequestURLs.GameplayRollDie, new PostRollDieDTO(gameboard));

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                // todo: do something
                return new BadRequestResult();
            }

            return RedirectToPage("./Index/", new { id = gameboard.ID, dieRoll = restResponse.Data.DieRoll });
        }

        public async Task<IActionResult> OnPostChooseMoveActionAsync(int? chosenMoveActionId, int? DieRoll)
        {
            if (chosenMoveActionId == null)
            {
                ModelState.AddModelError("PostRollDie_GameboardCurrentPlayer", "Gameboard.CurrentPlayer is null");

                return RedirectToPage("./Index/", new
                {
                    id = Gameboard.ID,
                    dieRoll = DieRoll,
                    moveActionMessage = "You have to select a move action!",
                });
            }

            var restResponse = await Fetch.PostAsync<TurnDataDTO>(Fetch.RequestURLs.GameplayChooseAction, chosenMoveActionId);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("PostChooseMoveAction.ResponseError.Debug", restResponse.ErrorMessage);
                ModelState.AddModelError("PostChooseMoveAction.ResponseError", restResponse.Data.Message);
                return Page();
            }

            return RedirectToPage("./Index/", new
            {
                id = Gameboard.ID,
                dieRoll = restResponse.Data.DieRoll,
                moveActionMessage = restResponse.Data.Message
            });
        }
    }
}
