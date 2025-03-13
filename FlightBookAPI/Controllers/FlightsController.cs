using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlight _flightRepository;

        public FlightsController(IFlight flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchFlights([FromQuery] string source, [FromQuery] string destination, [FromQuery] DateTime date)
        {
            var flights = await _flightRepository.SearchFlights(source, destination, date);
            return Ok(flights);
        }

        [HttpGet("searchBySourceDestination")]
        public async Task<ActionResult<IEnumerable<Flight>>> SearchFlightsBySourceDestination([FromQuery] string source, [FromQuery] string destination)
        {
            var flights = await _flightRepository.SearchFlightsBySourceDestination(source, destination);
            if (flights == null || !flights.Any())
            {
                return NotFound("No flights found for the specified source and destination.");
            }
            return Ok(flights);
        }

        [HttpGet("{flightId}")]
        public async Task<IActionResult> GetFlightById(int flightId)
        {
            var flight = await _flightRepository.GetFlightById(flightId);
            if (flight == null)
            {
                return NotFound("Flight not found");
            }
            return Ok(flight);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _flightRepository.GetAllFlight();
            if (flights != null && flights.Any())
            {
                return Ok(flights);
            }
            return NotFound("No flights found");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFlight([FromBody] Flight flight)
        {
            if (flight == null)
            {
                return BadRequest("Invalid flight data.");
            }
            var result = await _flightRepository.AddFlight(flight);
            return Ok(result);
        }

        [HttpPut("update/{flightId}")]
        public async Task<IActionResult> UpdateFlight(int flightId, [FromBody] Flight flight)
        {
            Console.WriteLine("Updating flight with data: " + flight);  // Debugging
            if (flightId != flight.FlightId)
            {
                return BadRequest("Flight ID mismatch.");
            }
            var result = await _flightRepository.UpdateFlight(flight);
            return result != null ? Ok(result) : NotFound("Flight not found");
        }




        [HttpDelete("delete/{flightId}")]
        public async Task<IActionResult> DeleteFlight(int flightId)
        {
            var result = await _flightRepository.DeleteFlight(flightId);
            return result ? Ok("Flight deleted") : NotFound("Flight not found");
        }

        [HttpGet("filterByAirline")]
        public async Task<IActionResult> FilterByAirline([FromQuery] string airline)
        {
            var flights = await _flightRepository.FilterByAirline(airline);
            if (flights == null || !flights.Any())
            {
                return NotFound("No flights found for the specified airline.");
            }
            return Ok(flights);
        }
    }
}
