using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBooking _bookingRepository;

        public BookingsController(IBooking bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookFlight([FromBody] Booking booking)
        {
            var result = await _bookingRepository.BookFlight(booking);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(int userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUser(userId);
            return Ok(bookings);
        }

        [HttpGet("reference/{referenceNumber}")]
        public async Task<IActionResult> GetBookingByReference(string referenceNumber)
        {
            var booking = await _bookingRepository.GetBookingByReference(referenceNumber);
            return booking != null ? Ok(booking) : NotFound("Booking not found");
        }
    }
}

