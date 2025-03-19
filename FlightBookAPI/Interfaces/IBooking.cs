using FlightBookAPI.Models;
using System.Threading.Tasks;

namespace FlightBookAPI.Interfaces
{
    public interface IBooking
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task CreateBookingAsync(Booking bookingRequest);// New method to save booking
        Task<List<Booking>> GetBookingsByUserIdAsync(int userId);

        Task DeleteBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(Booking booking);

    }
}
