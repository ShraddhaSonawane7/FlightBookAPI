using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInsController : ControllerBase
    {
        private readonly ICheckIn _checkInRepository;

        public CheckInsController(ICheckIn checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckInPassenger([FromBody] CheckIn checkIn)
        {
            var result = await _checkInRepository.CheckInPassenger(checkIn);
            return Ok(result);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetCheckInByBookingId(int bookingId)
        {
            var checkIn = await _checkInRepository.GetCheckInByBookingId(bookingId);
            return checkIn != null ? Ok(checkIn) : NotFound("Check-in record not found");
        }
    }
}

