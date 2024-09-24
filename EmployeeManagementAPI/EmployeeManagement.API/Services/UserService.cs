using EmployeeManagement.API.DataAccess;
using EmployeeManagement.API.Models;
using EmployeeManagment.API.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly EmployeeManagementDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(EmployeeManagementDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterUserDTO registerUserDto)
    {
        try
        {
            var existingUser = await _context.Users
                .AnyAsync(u => u.Username == registerUserDto.Username || u.Email == registerUserDto.Email);

            if (existingUser)
            {
                throw new Exception("Username or email already exists.");
            }

            var user = new User
            {
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                DateOfBirth = registerUserDto.DateOfBirth,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";  // Or return a meaningful error message
        }
    }

    public async Task<string> LoginAsync(LoginUserDTO loginUserDto)
    {
        try
        {
            var user = await _context.Users.SingleOrDefaultAsync(
                u => u.Username == loginUserDto.Username || u.Email == loginUserDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return "Invalid username or password.";
            }

            return GenerateJwtToken(user);
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    private string GenerateJwtToken(User user)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
