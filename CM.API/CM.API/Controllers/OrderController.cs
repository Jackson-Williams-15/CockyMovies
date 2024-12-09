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

    // Constructor to inject AppDbContext and ILogger for logging
    public OrderController(AppDbContext context, ILogger<OrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Get order details by order ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        _logger.LogInformation("Fetching order details for order ID: {OrderId}", id);

        // Fetch order with associated tickets, showtimes, and movies
        var order = await _context.OrderResult
            .Include(o => o.Tickets)               // Include tickets in the order
            .ThenInclude(t => t.Showtime)           // Include showtime for each ticket
            .ThenInclude(s => s.Movie)             // Include movie for each showtime
            .FirstOrDefaultAsync(o => o.Id == id); // Fetch the order by ID

        // If order not found, return NotFound response
        if (order == null)
        {
            _logger.LogWarning("Order not found: {OrderId}", id);
            return NotFound("Order not found.");
        }

        _logger.LogInformation("Fetched order details: {@Order}", order);
        return Ok(order); // Return the fetched order details
    }

    // Get all orders for a specific user
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(int userId)
    {
        _logger.LogInformation("Fetching orders for user ID: {UserId}", userId);

        // Fetch orders for the given user ID with associated tickets, showtimes, and movies
        var orders = await _context.OrderResult
            .Include(o => o.Tickets)               // Include tickets in each order
            .ThenInclude(t => t.Showtime)           // Include showtime for each ticket
            .ThenInclude(s => s.Movie)             // Include movie for each showtime
            .Where(o => o.UserId == userId)        // Filter orders by user ID
            .ToListAsync();                        // Retrieve orders for the user

        // If no orders found, return NotFound response
        if (orders == null || !orders.Any())
        {
            _logger.LogWarning("No orders found for user ID: {UserId}", userId);
            return NotFound("No orders found for this user.");
        }

        _logger.LogInformation("Fetched orders for user ID: {UserId}", userId);
        return Ok(orders); // Return the list of orders for the user
    }
}
