using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlightBookAPI.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly FlightDemoContext _context;

        public PaymentRepository(FlightDemoContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcessPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> GetPaymentByBookingId(int bookingId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }
    }
}



