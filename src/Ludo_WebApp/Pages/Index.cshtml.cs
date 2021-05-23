using Ludo_WebApp.Ludo_API;
using Ludo_WebApp.Models;
using Ludo_WebApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ludo_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public List<GameboardDTO> Gameboards { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            var restResponse = await Fetch.GetAsync<List<GameboardDTO>>(Fetch.RequestURLs.Games);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ModelState.AddModelError("ApiErrorGetGames", restResponse.Content);
                // todo: redirect to error page?
            }

            Gameboards = restResponse.Data;
        }
    }
}
