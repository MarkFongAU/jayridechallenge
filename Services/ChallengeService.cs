using CodingChallenge.Models;
using System.Net.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CodingChallenge.Services
{
    public static class CandidateService
    {
        public static Candidate GetCandidate() => new Candidate { Name = "test", Phone = "test" };
    }

    public static class ApiService
    {
        static ApiService()
        {

        }

        private static readonly HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromMilliseconds(60000) // 1min
        };

        public async static Task<T> CallApi<T>(string apiUrl, HttpMethod apiMethod)
        {
            T responseObject = default(T);

            try
            {
                //Build request
                HttpRequestMessage request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(apiUrl),
                    Method = HttpMethod.Get,
                };

                //Create response object
                HttpResponseMessage response = await client.SendAsync(request);

                //Get response
                var responseString = await response.Content.ReadAsStringAsync();

                // Process response code
                switch (response.StatusCode)
                {   
                    case HttpStatusCode.NotFound: //404, Could not find the asset
                    case HttpStatusCode.BadRequest: //400, Returns bad request response if request is invalid.
                    case HttpStatusCode.InternalServerError: //500, Exception happened while processing your request
                        throw new Exception($"API Call fail {response.StatusCode}");
                    default:
                        // Get deserialized response object
                        responseObject = JsonSerializer.Deserialize<T>(responseString);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseObject;
        }
    }
}