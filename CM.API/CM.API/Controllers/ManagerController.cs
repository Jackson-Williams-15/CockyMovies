using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ManagerController : ControllerBase
{
    [Authorize(Policy = "ManagerOnly")]
    [HttpGet("dashboard")]
    public IActionResult GetManagerDashboard()
    {
        return Ok(new { message = "Welcome to the manager dashboard!" });
    }
}