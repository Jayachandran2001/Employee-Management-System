using EmployeeManagment.API.DTO;
using EmployeeManagment.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public UsersController(IUserService userService, ITokenBlacklistService tokenBlacklistService)
        {
            _userService = userService;
            _tokenBlacklistService = tokenBlacklistService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            var token = await _userService.RegisterAsync(registerUserDto);
            if (token == null)
            {
                return BadRequest(new { message = "User registration failed" });
            }

            return Ok(new { message = token});
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {
            var token = await _userService.LoginAsync(loginUserDto);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                _tokenBlacklistService.AddToBlacklist(token);
            }

            return Ok(new { message = "Logout successful" });
        }
    }
}

