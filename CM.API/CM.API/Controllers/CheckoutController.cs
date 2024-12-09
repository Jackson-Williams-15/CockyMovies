using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ICartService _cartService; // Cart service dependency
    private readonly IPaymentService _paymentService; // Payment service dependency
    private readonly IEmailService _emailService; // Email service dependency
    private readonly AppDbContext _context; // Database context
    private readonly ILogger<CheckoutController> _logger; // Logger for tracking events

    // Constructor to initialize the CheckoutController with necessary services
    public CheckoutController(ICartService cartService, IPaymentService paymentService, AppDbContext context, ILogger<CheckoutController> logger, IEmailService emailService)
    {
        _cartService = cartService;
        _paymentService = paymentService;
        _context = context;
        _logger = logger;
        _emailService = emailService;
    }

    /// <summary>
    /// Processes the checkout, including payment processing and order creation.
    /// </summary>
    /// <param name="request">The checkout request containing cart ID, payment details, and user ID.</param>
    /// <returns>A response containing the order receipt if successful, or an error message if the process fails.</returns>
    [HttpPost("ProcessCheckout")]
    public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutRequestDto request)
    {
        _logger.LogInformation("Received checkout request: {@Request}", request);

        // Validate the model state
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state: {@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        // Retrieve the cart associated with the checkout request
        var cart = await _cartService.GetCartById(request.CartId);
        if (cart == null)
        {
            _logger.LogWarning("Cart not found: {CartId}", request.CartId);
            return NotFound("Cart not found.");
        }

        // Prepare payment details from the request
        var paymentDetails = new PaymentDetails
        {
            CardNumber = request.PaymentDetails.CardNumber,
            ExpiryDate = request.PaymentDetails.ExpiryDate,
            CVV = request.PaymentDetails.CVV,
            CardHolderName = request.PaymentDetails.CardHolderName,
            PaymentDate = DateTime.Now,
            Amount = cart.TotalPrice,
            PaymentMethod = "Credit Card"
        };

        // Process the payment using the payment service
        var paymentResult = await _paymentService.ProcessPayment(paymentDetails);
        if (!paymentResult)
        {
            _logger.LogWarning("Payment failed for cart: {CartId}", request.CartId);
            return BadRequest("Payment failed.");
        }

        // Mark tickets as sold in the cart
        foreach (var ticket in cart.Tickets)
        {
            if (ticket.Quantity <= 0)
            {
                _logger.LogWarning("Ticket quantity is zero or less for TicketId: {TicketId}", ticket.Id);
                continue;
            }

            ticket.IsSold = true;
            var ticketEntity = await _context.Ticket.FindAsync(ticket.Id);
            if (ticketEntity != null)
            {
                ticketEntity.IsSold = true;
                _context.Ticket.Update(ticketEntity);
            }
        }

        // Create the order details after successful payment
        var order = new OrderResult
        {
            CartId = request.CartId,
            ProcessedDate = DateTime.Now,
            Success = true,
            Details = "Order processed successfully.",
            TotalPrice = cart.TotalPrice,
            Tickets = new List<OrderTicket>(),
            UserId = request.UserId
        };

        // Save the order first so the OrderResultId is set for OrderTicket
        _context.OrderResult.Add(order);
        await _context.SaveChangesAsync();

        // Group tickets by showtime and movie
        var groupedTickets = cart.Tickets
            .GroupBy(t => new { t.ShowtimeId, t.MovieId })
            .Select(g => new
            {
                ShowtimeId = g.Key.ShowtimeId,
                MovieId = g.Key.MovieId,
                Tickets = g.ToList(),
                TotalQuantity = g.Sum(t => t.Quantity),
                TotalPrice = g.Sum(t => t.Price * t.Quantity)
            });

        // Create OrderTicket for each grouped ticket data
        foreach (var group in groupedTickets)
        {
            var showtime = await _context.Showtime.AsNoTracking().FirstOrDefaultAsync(s => s.Id == group.ShowtimeId);
            if (showtime == null)
            {
                _logger.LogWarning("Showtime not found for ShowtimeId: {ShowtimeId}", group.ShowtimeId);
                continue;
            }

            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == showtime.MovieId);
            if (movie == null)
            {
                _logger.LogWarning("Movie not found for ShowtimeId: {ShowtimeId}", showtime.Id);
                continue;
            }

            _logger.LogInformation("Creating OrderTicket for ShowtimeId: {ShowtimeId}, MovieId: {MovieId}, Quantity: {Quantity}", group.ShowtimeId, group.MovieId, group.TotalQuantity);

            // Ensure entities are tracked in the DbContext
            var trackedShowtime = await _context.Showtime.FindAsync(showtime.Id);
            var trackedMovie = await _context.Movies.FindAsync(movie.Id);

            if (trackedShowtime == null) _context.Attach(showtime);
            if (trackedMovie == null) _context.Attach(movie);

            var orderTicket = new OrderTicket
            {
                TicketId = group.Tickets.First().Id,
                ShowtimeId = group.ShowtimeId,
                MovieId = group.MovieId,
                Price = group.TotalPrice / group.TotalQuantity,
                Showtime = showtime,
                Movie = movie,
                OrderResultId = order.Id,
                Quantity = group.TotalQuantity
            };

            // Check if OrderTicket is already tracked
            var trackedOrderTicket = await _context.OrderTickets.FindAsync(orderTicket.OrderTicketId);
            if (trackedOrderTicket == null)
            {
                _context.OrderTickets.Add(orderTicket);
            }
            else
            {
                _context.Entry(trackedOrderTicket).CurrentValues.SetValues(orderTicket);
            }

            order.Tickets.Add(orderTicket);
            _logger.LogInformation("OrderTicket created: {@OrderTicket}", orderTicket);
        }

        // Save the OrderTickets
        await _context.SaveChangesAsync();

        // Clear the cart after successful checkout
        var cartEntity = await _context.Carts.FindAsync(cart.CartId);
        if (cartEntity != null)
        {
            cartEntity.Tickets.Clear();
            _context.Carts.Update(cartEntity);
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Checkout processed successfully for cart: {CartId}", request.CartId);

        // Generate the order receipt for the user
        var orderReceipt = await GenerateOrderReceipt(order);

        // Send order receipt via email
        var user = await _context.Users.FindAsync(request.UserId);
        if (user != null)
        {
            await _emailService.SendEmail(user.Email, EmailType.OrderReceipt, orderReceipt, user);
        }

        return Ok(orderReceipt);
    }

    /// <summary>
    /// Generates the order receipt for the processed order.
    /// </summary>
    /// <param name="order">The processed order details.</param>
    /// <returns>The generated order receipt.</returns>
    private async Task<OrderReceiptDto> GenerateOrderReceipt(OrderResult order)
    {
        var orderReceipt = new OrderReceiptDto
        {
            OrderId = order.Id,
            ProcessedDate = order.ProcessedDate,
            TotalPrice = order.TotalPrice,
            Tickets = order.Tickets?.Select(t => new OrderTicketDto
            {
                TicketId = t.TicketId,
                ShowtimeId = t.ShowtimeId,
                MovieTitle = t.Movie?.Title,
                ShowtimeStartTime = t.Showtime.StartTime,
                Price = t.Price,
                Quantity = t.Quantity
            }).ToList()
        };

        return orderReceipt;
    }
}
