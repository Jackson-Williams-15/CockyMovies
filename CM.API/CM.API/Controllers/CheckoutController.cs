using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    // Save order details
    var order = new OrderResult
    {
        OrderId = request.CartId,
        ProcessedDate = DateTime.Now,
        Success = true,
        Details = "Order processed successfully."
    };
    _context.OrderResult.Add(order);
    await _context.SaveChangesAsync();

    _logger.LogInformation("Checkout processed successfully for cart: {CartId}", request.CartId);
    return Ok(new { message = "Checkout processed successfully.", success = true });
}
}