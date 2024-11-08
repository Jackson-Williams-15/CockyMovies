using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly AppDbContext _context;
    private readonly ILogger<CheckoutController> _logger;

    public CheckoutController(ICartService cartService, IPaymentService paymentService, AppDbContext context, ILogger<CheckoutController> logger)
    {
        _cartService = cartService;
        _paymentService = paymentService;
        _context = context;
        _logger = logger;
    }

    [HttpPost("ProcessCheckout")]
    public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutRequestDto request)
    {
        _logger.LogInformation("Received checkout request: {@Request}", request);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state: {@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var cart = await _cartService.GetCartById(request.CartId);
        if (cart == null)
        {
            _logger.LogWarning("Cart not found: {CartId}", request.CartId);
            return NotFound("Cart not found.");
        }

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

        var paymentResult = await _paymentService.ProcessPayment(paymentDetails);
        if (!paymentResult)
        {
            _logger.LogWarning("Payment failed for cart: {CartId}", request.CartId);
            return BadRequest("Payment failed.");
        }

        // Mark tickets as sold
        foreach (var ticket in cart.Tickets)
        {
            ticket.IsSold = true;
            var ticketEntity = await _context.Ticket.FindAsync(ticket.Id);
            if (ticketEntity != null)
            {
                ticketEntity.IsSold = true;
                _context.Ticket.Update(ticketEntity);
            }
        }

        // Create order details
        var order = new OrderResult
        {
            CartId = request.CartId,
            ProcessedDate = DateTime.Now,
            Success = true,
            Details = "Order processed successfully.",
            TotalPrice = cart.TotalPrice,
            Tickets = new List<OrderTicket>(),
            UserId = request.UserId // Set the UserId
        };

        foreach (var ticket in cart.Tickets)
        {
            var showtime = await _context.Showtime.AsNoTracking().FirstOrDefaultAsync(s => s.Id == ticket.ShowtimeId);
            if (showtime == null)
            {
                _logger.LogWarning("Showtime not found for TicketId: {TicketId}, ShowtimeId: {ShowtimeId}", ticket.Id, ticket.ShowtimeId);
                continue;
            }

            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == showtime.MovieId);
            if (movie == null)
            {
                _logger.LogWarning("Movie not found for ShowtimeId: {ShowtimeId}", showtime.Id);
                continue;
            }

            _logger.LogInformation("Creating OrderTicket for TicketId: {TicketId}, ShowtimeId: {ShowtimeId}, MovieId: {MovieId}", ticket.Id, ticket.ShowtimeId, movie.Id);

            var orderTicket = new OrderTicket
            {
                TicketId = ticket.Id,
                ShowtimeId = ticket.ShowtimeId,
                MovieId = movie.Id,
                Price = ticket.Price,
                Showtime = showtime,
                Movie = movie
            };

            order.Tickets.Add(orderTicket);
        }

        _context.OrderResult.Add(order);

        // Clear the cart
        var cartEntity = await _context.Carts.FindAsync(cart.CartId);
        if (cartEntity != null)
        {
            cartEntity.Tickets.Clear();
            _context.Carts.Update(cartEntity);
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Checkout processed successfully for cart: {CartId}", request.CartId);
        return Ok(new { message = "Checkout processed successfully.", success = true, orderId = order.Id });
    }
}