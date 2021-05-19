using Ludo_WebApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ludo_WebApp.Ludo_API
{
    public static class Fetch
    {
        private const string _baseURL = "https://localhost:44376/api";

        private struct RequestURLs
        {
            internal static string NewGame = "/Games/New/";
        }

        //// Generic method for fetching data from the API (swapi.com)
        //public static async Task<List<T>> Data<T>(string requestUrl)
        //{
        //    try
        //    {
        //        var client = new RestClient(_baseURL);
        //        APIResponse<T> response;
        //        List<T> result = new();

        //        while (requestUrl != null)
        //        {
        //            string resource = requestUrl.Substring(_baseURL.Length);
        //            var request = new RestRequest(resource, DataFormat.Json);
        //            response = await client.GetAsync<APIResponse<T>>(request);

        //            result.AddRange(response.Results);
        //            requestUrl = response.Next;
        //        }

        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //}

        public static async Task<int> PostNewGameAsync(NewPlayerDTO newPlayerDTO)
        {
            //var postParking = new PostParking()
            //{
            //    Traveller = personName,
            //    StarShip = starshipsname,
            //    SpaceportId = spaceportId
            //};

            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.NewGame);
            request.AddJsonBody(newPlayerDTO);
            //request.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                //await client.PostAsync<GameDTO>(request);
                int response = await client.PostAsync<int>(request);
                //var response = await client.PostAsync<NewGameDTO>(request);
                return response;
            }
            catch (Exception e)
            {
                // log the error?
                // do error handling stuff?
                throw; // remove?
            }
        }

        public static async Task<int> PostAddPlayerAsync(NewPlayerDTO newPlayerDTO)
        {
            //var postParking = new PostParking()
            //{
            //    Traveller = personName,
            //    StarShip = starshipsname,
            //    SpaceportId = spaceportId
            //};

            var client = new RestClient(_baseURL);
            var request = new RestRequest(RequestURLs.NewGame);
            request.AddJsonBody(newPlayerDTO);
            //request.OnBeforeDeserialization = r => { r.ContentType = "application/json"; };

            try // todo: fix getting and returning the error msg from throw in SetTrack
            {
                //await client.PostAsync<GameDTO>(request);
                int response = await client.PostAsync<int>(request);
                //var response = await client.PostAsync<NewGameDTO>(request);
                return response;
            }
            catch (Exception e)
            {
                // log the error?
                // do error handling stuff?
                throw; // remove?
            }
        }
    }
}
