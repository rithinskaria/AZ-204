using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AirportCodeAPI.Models;

namespace AirportCodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportCodesController : ControllerBase
    {
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "airports.json");

        [HttpGet]
        public IEnumerable<AirportCode> Get()
        {
            var airportCodes = LoadAirportCodesFromJson();
            return airportCodes;
        }

        private IEnumerable<AirportCode> LoadAirportCodesFromJson()
        {
            if (!System.IO.File.Exists(_jsonFilePath))
            {
                return []; 
            }

            var json = System.IO.File.ReadAllText(_jsonFilePath);
            var airportCodes = JsonSerializer.Deserialize<IEnumerable<AirportCode>>(json);
            return airportCodes ?? [];
        }
    }
}
