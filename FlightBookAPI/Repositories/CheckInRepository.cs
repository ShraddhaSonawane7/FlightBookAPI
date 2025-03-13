using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlightBookAPI.Repositories
{
    public class CheckInRepository : ICheckIn
    {
        private readonly FlightDemoContext  _context;

        public CheckInRepository(FlightDemoContext context)
        {
            _context = context;
        }

        public async Task<CheckIn> CheckInPassenger(CheckIn checkIn)
        {
            _context.CheckIns.Add(checkIn);
            await _context.SaveChangesAsync();
            return checkIn;
        }

        public async Task<CheckIn> GetCheckInByBookingId(int bookingId)
        {
            return await _context.CheckIns.FirstOrDefaultAsync(c => c.BookingId == bookingId);
        }
    }
}

