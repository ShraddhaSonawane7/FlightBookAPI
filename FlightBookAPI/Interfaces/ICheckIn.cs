using FlightBookAPI.Models;
using System.Threading.Tasks;

namespace FlightBookAPI.Interfaces
{
    public interface ICheckIn
    {
        Task<CheckIn> CheckInPassenger(CheckIn checkIn);
        Task<CheckIn> GetCheckInByBookingId(int bookingId);
    }
}