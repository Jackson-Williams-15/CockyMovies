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
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _context;
    private readonly ILogger<CheckoutController> _logger;

    public CheckoutController(ICartService cartService, IPaymentService paymentService, AppDbContext context, ILogger<CheckoutController> logger, IEmailService emailService)
    {
        _cartService = cartService;
        _paymentService = paymentService;
        _context = context;
        _logger = logger;
        _emailService = emailService;
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

        // Create order details
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

        // Save OrderResult FIRST so the OrderResultId is set for the OrderTicket.
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

            // This checks if the entities are already tracked in the dbcontext
            var trackedShowtime = await _context.Showtime.FindAsync(showtime.Id);
            var trackedMovie = await _context.Movies.FindAsync(movie.Id);

            if (trackedShowtime == null)
            {
                _context.Attach(showtime);
            }
            else
            {
                showtime = trackedShowtime;
            }

            if (trackedMovie == null)
            {
                _context.Attach(movie);
            }
            else
            {
                movie = trackedMovie;
            }

            var orderTicket = new OrderTicket
            {
                // Assign to group tickets, showtimes, movie, and price
                TicketId = group.Tickets.First().Id, // Use first ticket's ID
                ShowtimeId = group.ShowtimeId,
                MovieId = group.MovieId,
                Price = group.TotalPrice / group.TotalQuantity, // Average price per ticket
                Showtime = showtime,
                Movie = movie,
                OrderResultId = order.Id, // Associate with OrderResult
                Quantity = group.TotalQuantity // Set the total quantity
            };

            // Make sure  the OrderTicket is not already tracked in the dbcontext
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

        // Clear the cart
        var cartEntity = await _context.Carts.FindAsync(cart.CartId);
        if (cartEntity != null)
        {
            cartEntity.Tickets.Clear();
            _context.Carts.Update(cartEntity);
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Checkout processed successfully for cart: {CartId}", request.CartId);

        var orderReceipt = await GenerateOrderReceipt(order);

    // Send order receipt email
    var user = await _context.Users.FindAsync(request.UserId);
    if (user != null)
    {
        await SendOrderReceiptEmail(user, orderReceipt);
    }

        return Ok(orderReceipt);
    }

    private async Task<OrderReceiptDto> GenerateOrderReceipt(OrderResult order) {
        var orderReceipt = new OrderReceiptDto
    {
        OrderId = order.Id,
        ProcessedDate = order.ProcessedDate,
        TotalPrice = order.TotalPrice,
        Tickets = order.Tickets.Select(t => new OrderTicketDto
        {
            TicketId = t.TicketId,
            ShowtimeId = t.ShowtimeId,
            MovieTitle = t.Movie.Title,
            ShowtimeStartTime = t.Showtime.StartTime,
            Price = t.Price,
            Quantity = t.Quantity
        }).ToList()
    };

    return orderReceipt;
    } 

    private async Task SendOrderReceiptEmail(User user, OrderReceiptDto orderReceipt)
{
    var emailBody = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                margin: 0;
                padding: 0;
                background-color: #f9f9f9;
            }}
            .email-container {{
                max-width: 600px;
                margin: 20px auto;
                padding: 20px;
                background-color: #ffffff;
                border-radius: 10px;
                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            }}
            .header {{
                font-size: 20px;
                font-weight: bold;
                text-align: center;
                margin-bottom: 20px;
                color: #007BFF;
            }}
            .order-details {{
                margin-bottom: 20px;
            }}
            .order-details h3 {{
                font-size: 18px;
                margin-bottom: 10px;
                color: #444;
            }}
            .ticket {{
                margin-bottom: 10px;
                padding: 10px;
                border-bottom: 1px solid #e0e0e0;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='header'>
                Thank you for your order, {user.Username}!
            </div>
            <div class='order-details'>
                <h3>Order Summary</h3>
                <p><strong>Order ID:</strong> {orderReceipt.OrderId}</p>
                <p><strong>Processed Date:</strong> {orderReceipt.ProcessedDate}</p>
                <p><strong>Total Price:</strong> ${orderReceipt.TotalPrice:F2}</p>
            </div>
            <div class='tickets'>
                <h3>Tickets</h3>";
                
    foreach (var ticket in orderReceipt.Tickets)
    {
        emailBody += $@"
                <div class='ticket'>
                    <p><strong>Movie:</strong> {ticket.MovieTitle}</p>
                    <p><strong>Showtime:</strong> {ticket.ShowtimeStartTime}</p>
                    <p><strong>Quantity:</strong> {ticket.Quantity}</p>
                    <p><strong>Price per Ticket:</strong> ${ticket.Price:F2}</p>
                    <p><strong>Total Price:</strong> ${(ticket.Price * ticket.Quantity):F2}</p>
                </div>";
    }

    emailBody += @"
            </div>
            <div class='footer'>
                This email was generated automatically. For assistance, please contact our support team.
            </div>
        </div>
    </body>
    </html>";

    await _emailService.SendEmail(user.Email, "Your Order Receipt", emailBody);
}

}