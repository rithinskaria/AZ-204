using Microsoft.AspNetCore.Mvc;
using AirportLogApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AirportLogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightLogsController : ControllerBase
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "airportLogs.json");

        // Read JSON
        private List<FlightLog> GetFlightLogsFromFile()
        {
            var jsonData = System.IO.File.ReadAllText(_filePath);
            var flightLogs = JsonConvert.DeserializeObject<List<FlightLog>>(jsonData);
            return flightLogs;
        }

        // Write JSON
        private void WriteFlightLogsToFile(List<FlightLog> flightLogs)
        {
            var jsonData = JsonConvert.SerializeObject(flightLogs, Formatting.Indented); // Indented for readability
            System.IO.File.WriteAllText(_filePath, jsonData); // Write the updated data back to the JSON file
        }


        // GET: api/flightlogs
        [HttpGet]
        public ActionResult<IEnumerable<FlightLog>> GetFlightLogs()
        {
            var flightLogs = GetFlightLogsFromFile();
            return Ok(flightLogs);
        }

        // GET: api/flightlogs/{flightNumber}
        [HttpGet("{flightNumber}")]
        public ActionResult<FlightLog> GetFlightLog(int flightNumber)
        {
            var flightLogs = GetFlightLogsFromFile();
            var flightLog = flightLogs.FirstOrDefault(f => f.FlightNumber == flightNumber);
            if (flightLog == null)
            {
                return NotFound();
            }
            return Ok(flightLog);
        }

        // POST: api/flightlogs
        [HttpPost]
        public ActionResult<FlightLog> PostFlightLog(FlightLog newFlightLog)
        {
            var flightLogs = GetFlightLogsFromFile();
            flightLogs.Add(newFlightLog);

            // Update JSON file
            WriteFlightLogsToFile(flightLogs);

            return CreatedAtAction(nameof(GetFlightLog), new { flightNumber = newFlightLog.FlightNumber }, newFlightLog);
        }
   [HttpPut("{flightNumber}")]
public IActionResult PutFlightLog(int flightNumber, FlightLog updatedFlightLog)
{
    Console.WriteLine($"File path: {_filePath}");  // Log the file path

    var flightLogs = GetFlightLogsFromFile(); // Load flight logs
    var existingFlightLog = flightLogs.FirstOrDefault(f => f.FlightNumber == flightNumber);

    if (existingFlightLog == null)
    {
        Console.WriteLine("Flight not found.");
        return NotFound();
    }

    Console.WriteLine($"Updating flight: {flightNumber}");

    // Update fields
    existingFlightLog.DepartureAirportCode = updatedFlightLog.DepartureAirportCode;
    existingFlightLog.ArrivalAirportCode = updatedFlightLog.ArrivalAirportCode;
    existingFlightLog.DepartureDatetime = updatedFlightLog.DepartureDatetime;
    existingFlightLog.ArrivalDatetime = updatedFlightLog.ArrivalDatetime;
    existingFlightLog.AirlineName = updatedFlightLog.AirlineName;
    existingFlightLog.FlightDurationMinutes = updatedFlightLog.FlightDurationMinutes;

    // Write back to the file
    WriteFlightLogsToFile(flightLogs);
    Console.WriteLine("File updated successfully.");

    return NoContent();
}


        // DELETE: api/flightlogs/{flightNumber}
        [HttpDelete("{flightNumber}")]
        public IActionResult DeleteFlightLog(int flightNumber)
        {
            var flightLogs = GetFlightLogsFromFile();
            var flightLog = flightLogs.FirstOrDefault(f => f.FlightNumber == flightNumber);
            if (flightLog == null)
            {
                return NotFound();
            }

            // Delete from JSON
            flightLogs.Remove(flightLog);

            // Save JSON
            WriteFlightLogsToFile(flightLogs);

            return NoContent();
        }
    }
}
