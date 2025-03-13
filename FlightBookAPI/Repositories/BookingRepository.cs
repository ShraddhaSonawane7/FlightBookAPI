using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookAPI.Repositories
{
    public class BookingRepository : IBooking
    {
        private readonly FlightDemoContext _context;

        public BookingRepository(FlightDemoContext context)
        {
            _context = context;
        }

        public async Task<Booking> BookFlight(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUser(int userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Booking> GetBookingByReference(string referenceNumber)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.ReferenceNumber == referenceNumber);
        }
    }
}

