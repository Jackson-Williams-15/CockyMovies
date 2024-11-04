using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
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
            OrderId = request.CartId,
            ProcessedDate = DateTime.Now,
            Success = true,
            Details = "Order processed successfully.",
            TotalPrice = cart.TotalPrice,
            Tickets = cart.Tickets.Select(t => new OrderTicket
            {
                TicketId = t.Id,
                ShowtimeId = t.ShowtimeId,
                Price = t.Price,
                Showtime = _context.Showtime.FirstOrDefault(s => s.Id == t.ShowtimeId),
                Movie = _context.Movies.FirstOrDefault(m => m.Id == t.Showtime.Movie.Id)
            }).ToList(),
            UserId = request.UserId // Set the UserId
        };

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
        return Ok(new { message = "Checkout processed successfully.", success = true });
    }
}