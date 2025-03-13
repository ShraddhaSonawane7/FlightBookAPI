using FlightBookAPI.Models;
using System.Threading.Tasks;

namespace FlightBookAPI.Interfaces
{
    public interface IPayment
    {
        Task<Payment> ProcessPayment(Payment payment);
        Task<Payment> GetPaymentByBookingId(int bookingId);
    }
}

