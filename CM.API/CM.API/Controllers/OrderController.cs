using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<OrderController> _logger;

    public OrderController(AppDbContext context, ILogger<OrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        _logger.LogInformation("Fetching order details for order ID: {OrderId}", id);

        var order = await _context.OrderResult
            .Include(o => o.Tickets)
            .ThenInclude(t => t.Showtime)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            _logger.LogWarning("Order not found: {OrderId}", id);
            return NotFound("Order not found.");
        }

        _logger.LogInformation("Fetched order details: {@Order}", order);
        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(int userId)
    {
        _logger.LogInformation("Fetching orders for user ID: {UserId}", userId);

        var orders = await _context.OrderResult
            .Include(o => o.Tickets)
            .ThenInclude(t => t.Showtime)
            .ThenInclude(s => s.Movie)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        if (orders == null || !orders.Any())
        {
            _logger.LogWarning("No orders found for user ID: {UserId}", userId);
            return NotFound("No orders found for this user.");
        }

        _logger.LogInformation("Fetched orders for user ID: {UserId}", userId);
        return Ok(orders);
    }
}