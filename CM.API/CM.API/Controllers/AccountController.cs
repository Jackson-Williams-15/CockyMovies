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

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signupRequest)
        {
            var user = await _accountService.Authenticate(signupRequest.Username, signupRequest.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(user);
        }
    }

    public class SignUpRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}