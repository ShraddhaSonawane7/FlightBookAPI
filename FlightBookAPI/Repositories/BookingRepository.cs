using FlightBookAPI.Interfaces;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
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

        [Authorize(Roles ="Passenger")]
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                                 .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

   
        public async Task CreateBookingAsync(Booking bookingRequest)
        {
            // Log the data before saving
            Console.WriteLine($"Saving Booking: {bookingRequest.UserId}, {bookingRequest.FlightId}, {bookingRequest.TotalPrice}");

            // Insert the booking into the database
            await _context.Bookings.AddAsync(new Booking
            {
                UserId = bookingRequest.UserId,
                FlightId = bookingRequest.FlightId,
                SeatType = bookingRequest.SeatType,
                ClassType = bookingRequest.ClassType,
                Adults = bookingRequest.Adults,
                Children = bookingRequest.Children,
                Seniors = bookingRequest.Seniors,
                TotalPrice = bookingRequest.TotalPrice,
                BookingDate = DateTime.Now // Assuming current date for booking date
            });

            await _context.SaveChangesAsync(); // Ensure the changes are saved asynchronously
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(); // Make sure to save asynchronously
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(booking.BookingId);
            if (existingBooking == null)
            {
                return false;  // Booking not found
            }

            // Update relevant fields
            existingBooking.ClassType = booking.ClassType;
            existingBooking.Adults = booking.Adults;
            existingBooking.Children = booking.Children;
            existingBooking.Seniors = booking.Seniors;
            existingBooking.SeatType = booking.SeatType;
            existingBooking.TotalPrice = CalculateTotalPrice(existingBooking);

            await _context.SaveChangesAsync();
            return true;
        }

        // Calculate total price based on booking details
        private decimal CalculateTotalPrice(Booking booking)
        {
            decimal classPrice = booking.ClassType switch
            {
                "Economy" => 100m,
                "Business" => 250m,
                "First Class" => 400m,
                _ => 100m
            };

            decimal childrenPrice = classPrice * 0.5m;  // Discount for children
            decimal seniorPrice = classPrice * 0.7m;   // Discount for seniors

            return (booking.Adults * classPrice) +
                   (booking.Children * childrenPrice) +
                   (booking.Seniors * seniorPrice);
        }
    }

}
