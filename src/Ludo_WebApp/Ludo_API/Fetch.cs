﻿using Ludo_WebApp.Ludo_API.Models;
using Ludo_WebApp.Models;
using Ludo_WebApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ludo_WebApp.Ludo_API
{
    public static class Fetch
    {
        private const string _baseURL = "https://localhost:44376/api";

        internal struct RequestURLs
        {
            // Games endpoint/controller:
            internal static string Games = "/Games/";
            internal static string GamesLudoData = "/Games/LudoData/";
            internal static string GamesNew = "/Games/New/";
            internal static string GamesAddPlayer = "/Games/AddPlayer/";
            internal static string GamesStartGame = "/Games/StartGame/";
            // Gameplay endpoint/controller:
            internal static string GameplayRollDie = "/Gameplay/RollDie/";
            internal static string GameplayGetMoveActions = "/Gameplay/GetMoveActions/";
            internal static string GameplayChooseAction = "/Gameplay/ChooseAction/";
        }

        public static async Task<IRestResponse<GameboardDTO>> PostNewGameAsync(NewPlayerDTO newPlayerDTO)
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.GamesNew, Method.POST);
            request.AddJsonBody(newPlayerDTO);

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                var response = await client.ExecuteAsync<GameboardDTO>(request);

                return response;
            }
            catch (Exception)
            {
                // log the error?
                // do error handling stuff?
                throw; // remove?
            }
        }

        public static async Task<IRestResponse<NewPlayerDTO>> PostAddPlayerAsync(NewPlayerDTO newPlayerDTO)
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.GamesAddPlayer, Method.POST);
            request.AddJsonBody(newPlayerDTO);

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                return await client.ExecuteAsync<NewPlayerDTO>(request);
            }
            catch (Exception)
            {
                // log the error?
                // do error handling stuff?
                throw; // remove?
            }
        }

        internal static async Task<IRestResponse<List<GameboardDTO>>> GetGames()
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.Games, Method.GET);

            try
            {
                return await client.ExecuteAsync<List<GameboardDTO>>(request);
            }
            catch (Exception)
            {
                // log the error?
                return null;
            }
        }

        // api/Games/{id}
        internal static async Task<IRestResponse<GameboardDTO>> GetGame(int gameId)
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.Games + gameId, Method.GET);

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                return await client.ExecuteAsync<GameboardDTO>(request);
            }
            catch (Exception)
            {
                // log the error?
                // do error handling stuff?
                return null;
            }
        }

        internal static async Task<IRestResponse<LudoData>> GetLudoData()
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.GamesLudoData, Method.GET);

            try
            {
                return await client.ExecuteAsync<LudoData>(request);
            }
            catch (Exception)
            {
                // log the error?
                return null;
            }
        }

        internal static async Task<IRestResponse<GameboardDTO>> StartGameAsync(int gameId)
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.GamesStartGame, Method.POST);
            request.AddJsonBody(gameId);

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                return await client.ExecuteAsync<GameboardDTO>(request);
            }
            catch (Exception)
            {
                // log the error?
                // do error handling stuff?
                return null;
            }
        }
        /* -----------------------------------------------------------------------------*/
        /* New generic methods                                                          */
        /* -----------------------------------------------------------------------------*/

        internal static async Task<IRestResponse<T>> GetAsync<T>(string requestURL, object queryParameters)
        {
            // The source code for RouteValueDictionary as used by the RedirectToPage class like so: 'RedirectToPage("./Index/", new { id = 5 }),
            // was used to figured out how to convert an object of keys and values to a dictionary<string, string> of keys and values.
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (queryParameters != null)
            {
                PropertyDescriptorCollection @params = TypeDescriptor.GetProperties(queryParameters);
                foreach (PropertyDescriptor param in @params)
                {
                    dict.Add(param.Name, param.GetValue(queryParameters));
                }
            }

            string queryString = dict?.Count > 0 ? "?" + string.Join("&", dict.Select(param => param.Key + "=" + param.Value)) : "";

            var client = new RestClient(_baseURL);
            var request = new RestRequest(requestURL + queryString, Method.GET);

            try
            {
                return await client.ExecuteAsync<T>(request);
            }
            catch (Exception)
            {
                // log the error?
                return null;
            }
        }

        internal static async Task<IRestResponse<T>> GetAsync<T>(string requestURL, int? id = null)
        {

            var client = new RestClient(_baseURL);
            var request = new RestRequest(requestURL + id ?? "", Method.GET);

            try
            {
                return await client.ExecuteAsync<T>(request);
            }
            catch (Exception)
            {
                // log the error?
                return null;
            }
        }

        internal static async Task<IRestResponse<T1>> PostAsync<T1>(string requestURL, object body)
        {
            var client = new RestClient(_baseURL);
            var request = new RestRequest(requestURL, Method.POST);
            request.AddJsonBody(body);

            try
            {
                return await client.ExecuteAsync<T1>(request);
            }
            catch (Exception)
            {
                // log the error?
                return null;
            }
        }
    }
}
