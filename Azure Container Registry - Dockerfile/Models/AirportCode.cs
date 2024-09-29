using System.Text.Json.Serialization;

namespace AirportCodeAPI.Models
{
    public class AirportCode
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("airport_name")]
        public required string AirportName { get; set; }

        [JsonPropertyName("country_code")]
        public required string CountryCode { get; set; }
    }
}

