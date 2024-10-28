using CM.API.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private List<Cart> _carts = new List<Cart>();
    private List<Ticket> _tickets = new List<Ticket>();

    [HttpPost("AddTicketToCart")]
    public IActionResult AddTicketToCart(int cartId, int ticketId)
    {
        // Find the ticket
        var ticket = _tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return NotFound("Ticket not found.");
        }

        // Find or create a new cart
        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            cart = new Cart { CartId = cartId };
            _carts.Add(cart);
        }

        // Add ticket to the cart
        cart.Tickets.Add(ticket);

        return Ok(new { message = "Ticket added to cart successfully.", success = true });
    }

    [HttpGet("GetCartById/{cartId}")]
    public IActionResult GetCartById(int cartId)
    {
        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        return Ok(cart);
    }
}
