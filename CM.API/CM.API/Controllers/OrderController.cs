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
    private readonly AppDbContext _context; // Database context
    private readonly ILogger<OrderController> _logger; // Logger for tracking events

    // Constructor to initialize the OrderController with necessary dependencies
    public OrderController(AppDbContext context, ILogger<OrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Fetches the details of a specific order based on its ID.
    /// </summary>
    /// <param name="id">The ID of the order to fetch.</param>
    /// <returns>An IActionResult containing the order details if found, or an error message if not.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        _logger.LogInformation("Fetching order details for order ID: {OrderId}", id);

        // Fetch the order with its associated tickets, showtimes, and movies
        var order = await _context.OrderResult
            .Include(o => o.Tickets) // Include the tickets associated with the order
                .ThenInclude(t => t.Showtime) // Include the showtime for each ticket
                    .ThenInclude(s => s.Movie) // Include the movie for each showtime
            .FirstOrDefaultAsync(o => o.Id == id); // Find the order by ID

        if (order == null)
        {
            _logger.LogWarning("Order not found: {OrderId}", id);
            return NotFound("Order not found.");
        }

        _logger.LogInformation("Fetched order details: {@Order}", order);
        return Ok(order); // Return the order details
    }

    /// <summary>
    /// Fetches all orders associated with a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose orders are to be fetched.</param>
    /// <returns>An IActionResult containing the user's orders if found, or an error message if no orders exist.</returns>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(int userId)
    {
        _logger.LogInformation("Fetching orders for user ID: {UserId}", userId);

        // Fetch all orders for the specified user, including associated tickets, showtimes, and movies
        var orders = await _context.OrderResult
            .Include(o => o.Tickets) // Include the tickets associated with the order
                .ThenInclude(t => t.Showtime) // Include the showtime for each ticket
                    .ThenInclude(s => s.Movie) // Include the movie for each showtime
            .Where(o => o.UserId == userId) // Filter orders by user ID
            .ToListAsync(); // Execute the query asynchronously

        if (orders == null || !orders.Any())
        {
            _logger.LogWarning("No orders found for user ID: {UserId}", userId);
            return NotFound("No orders found for this user.");
        }

        _logger.LogInformation("Fetched orders for user ID: {UserId}", userId);
        return Ok(orders); // Return the list of orders for the user
    }
}
