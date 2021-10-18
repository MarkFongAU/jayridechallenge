
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodingChallenge.Models
{
    //This implementation broke the SOLID principle "I", but I am on a timer.
    public class Trip
    {
        [JsonPropertyName("from")] public string From { get; set; }
        [JsonPropertyName("to")] public string To { get; set; }
        [JsonPropertyName("listings")] public List<ListingWithPrice> ListingWithPrice { get; set; }
    }
    public class Listing
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("pricePerPassenger")] public double PricePerPassenger { get; set; }
        [JsonPropertyName("vehicleType")] public VehicleType VehicleType { get; set; }
    }

    public class VehicleType
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("maxPassengers")] public int MaxPassengers { get; set; }
    }

    public class ListingWithPrice : Listing
    {
        public int PassengerCount { get; set; }
        public double TotalPrice => Math.Round(PassengerCount * PricePerPassenger, 2); //to 2 decimal
    }
}