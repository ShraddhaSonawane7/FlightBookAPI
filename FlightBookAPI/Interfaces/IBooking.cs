using FlightBookAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookAPI.Interfaces
{
    public interface IBooking
    {
        Task<Booking> BookFlight(Booking booking);
        Task<IEnumerable<Booking>> GetBookingsByUser(int userId);
        Task<Booking> GetBookingByReference(string referenceNumber);
    }
}