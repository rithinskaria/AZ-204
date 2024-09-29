using Newtonsoft.Json;
using System;

namespace AirportLogApi.Models
{
    public class FlightLog
    {
        [JsonProperty("flight_number")]
        public int FlightNumber { get; set; }

        [JsonProperty("departure_airport_code")]
        public required string DepartureAirportCode { get; set; }

        [JsonProperty("arrival_airport_code")]
        public required string ArrivalAirportCode { get; set; }

        [JsonProperty("departure_datetime")]
        public DateTime DepartureDatetime { get; set; }

        [JsonProperty("arrival_datetime")]
        public DateTime ArrivalDatetime { get; set; }

        [JsonProperty("airline_name")]
        public required string AirlineName { get; set; }

        [JsonProperty("flight_duration_minutes")]
        public int FlightDurationMinutes { get; set; }
    }
}

