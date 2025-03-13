using FlightBookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserRepository : IUser
{
    private readonly FlightDemoContext _context;

    public UserRepository(FlightDemoContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
