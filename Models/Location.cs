using System.Text.Json.Serialization;

namespace CodingChallenge.Models
{
    public class Location
    {
        [JsonPropertyName("ip")] public string IpAddress { get; set; }

        [JsonPropertyName("city")] public string CityLocation { get; set; }
    }
}