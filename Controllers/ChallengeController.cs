using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CodingChallenge.Models;
using CodingChallenge.Services;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace CodingChallenge.Controllers
{
    [ApiController]
    [Route("candidate")]
    public class CandidateController : ControllerBase
    {
        //Task 1: GET /candidate
        [HttpGet]
        public ActionResult<Candidate> GetCandidate() => CandidateService.GetCandidate();
    }

    [ApiController]
    [Route("location")]
    public class LocationController : ControllerBase
    {
        //Task 2: GET /Location
        [HttpGet]
        public async Task<ActionResult<Location>> GetLocation(string ip)
        {
            try
            {
                if (string.IsNullOrEmpty(ip))
                    return BadRequest();

                string apiKey = System.IO.File.ReadAllText("apiKey.txt");
                string apiUri = $"http://api.ipstack.com/{ip}?access_key={apiKey}";

                var location = await ApiService.CallApi<Location>(apiUri, HttpMethod.Get);

                if (location is null)
                    return NotFound();

                return location;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

    [ApiController]
    [Route("listings")]
    public class ListingController : ControllerBase
    {
        //Task 3: Get /Listings
        public async Task<ActionResult<List<ListingWithPrice>>> GetListing(string passengerNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(passengerNumber))
                    return BadRequest();

                int passengerCount = Convert.ToInt32(passengerNumber);

                string apiUri = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";
                var trip = await ApiService.CallApi<Trip>(apiUri, HttpMethod.Get);

                if (trip is null)
                    return NotFound();

                //Filter vehicles that cant fit the passenger count
                var trimmedListing = trip.ListingWithPrice.Where(l => passengerCount <= l.VehicleType.MaxPassengers);
                foreach (var listing in trimmedListing)
                {
                    listing.PassengerCount = passengerCount;
                }

                //Order by total price
                return trimmedListing.OrderBy(l => l.TotalPrice).ToList();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}