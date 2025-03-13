using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPayment _paymentRepository;

        public PaymentsController(IPayment paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] Payment payment)
        {
            var result = await _paymentRepository.ProcessPayment(payment);
            return Ok(result);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetPaymentByBookingId(int bookingId)
        {
            var payment = await _paymentRepository.GetPaymentByBookingId(bookingId);
            return payment != null ? Ok(payment) : NotFound("Payment not found");
        }
    }
}

