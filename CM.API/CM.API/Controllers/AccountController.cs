using System.Threading.Tasks;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserCreateDto signupRequest)
        {
            var user = await _accountService.Register(signupRequest.Email, signupRequest.Username, signupRequest.Password, signupRequest.DateOfBirth);

            if (user == null)
                return BadRequest(new { message = "User registration failed" });

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                DateOfBirth = user.DateOfBirth
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
        {
            var user = await _accountService.Authenticate(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                DateOfBirth = user.DateOfBirth
            };

            return Ok(userDto);
        }
    }
}