using Microsoft.AspNetCore.Mvc;
using FlightBookAPI.Models;
using FlightBookAPI.Interfaces;
using System.Threading.Tasks;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FlightBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking _bookingRepository;

        public BookingController(IBooking bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [Authorize(Roles = "Passenger")]
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] Booking bookingRequest)
        {
            if (bookingRequest == null)
            {
                return BadRequest("Invalid booking data.");
            }

            // Log the data to check if it's coming correctly
            Console.WriteLine($"Received Booking Request: {bookingRequest.UserId}, {bookingRequest.FlightId}");

            // Save the data to the database asynchronously
            await _bookingRepository.CreateBookingAsync(bookingRequest);

            return Ok(new { message = "Booking confirmed successfully." });
        }

        [Authorize] // Ensure only authenticated users can access
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);

            if (bookings == null || !bookings.Any())
            {
                return NotFound(new { message = "No bookings found for this user." });
            }

            return Ok(bookings);
        }

        [Authorize]
        [HttpDelete("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound(new { message = "Booking not found." });
            }

            // Call the asynchronous method to delete the booking
            await _bookingRepository.DeleteBookingAsync(booking);

            return Ok(new { message = "Booking cancelled successfully." });
        }

        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest("Booking ID mismatch.");
            }

            var isUpdated = await _bookingRepository.UpdateBookingAsync(booking);

            if (!isUpdated)
            {
                return NotFound("Booking not found.");
            }

            return NoContent();  // Return HTTP 204 No Content on successful update
        }
    }

}

