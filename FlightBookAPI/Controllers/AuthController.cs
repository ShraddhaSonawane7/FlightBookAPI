using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FlightBookAPI.Models;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUser _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IUser userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // ✅ Passenger Registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User model)
    {
        if (await _userRepository.GetUserByEmailAsync(model.Email) != null)
        {
            return BadRequest(new { message = "Email already exists." });
        }

        // Hash the password before saving
        model.PasswordHash = HashPassword(model.PasswordHash);
        model.Role = "Passenger"; // ✅ Assign Passenger role

        await _userRepository.AddUserAsync(model);
        return Ok(new { message = "Passenger registered successfully!" });
    }

    // ✅ User Login (Admin & Passenger)
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest(new { message = "Email and password are required." });
        }

        var user = await _userRepository.GetUserByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        // Hash the input password using SHA-256
        string hashedInputPassword = HashPassword(model.Password);

        if (user.PasswordHash != hashedInputPassword)  // Compare with DB hash
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        // Generate JWT Token
        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    // ✅ JWT Token Generation
    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]); // ✅ FIXED PATH
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // ✅ SHA256 Password Hashing (Matches SQL Server)
    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper(); // ✅ Matches SQL Server
        }
    }
}
