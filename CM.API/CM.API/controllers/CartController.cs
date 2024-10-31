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
        var ticket = _tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return NotFound("Ticket not found.");
        }

        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            cart = new Cart { CartId = cartId };
            _carts.Add(cart);
        }

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

    [HttpDelete("RemoveTicketFromCart")]
    public IActionResult RemoveTicketFromCart(int cartId, int ticketId)
    {
        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        var ticket = cart.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return NotFound("Ticket not found in cart.");
        }

        cart.Tickets.Remove(ticket);

        return Ok(new { message = "Ticket removed from cart successfully.", success = true });
    }
}
