using FlightBookAPI.Models;
using System.Threading.Tasks;

public interface IUser
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}
