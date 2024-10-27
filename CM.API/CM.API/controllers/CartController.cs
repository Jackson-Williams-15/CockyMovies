// Controllers/CartController.cs
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("AddTicketToCart")]
    public IActionResult AddTicketToCart(int cartId, int ticketId, int quantity)
    {
        var success = _cartService.AddTicketToCart(cartId, ticketId, quantity);
        if (!success)
        {
            return BadRequest("Failed to add ticket to cart. Either the ticket was not found or the quantity is invalid.");
        }

        return Ok(new { message = "Tickets added to cart successfully.", success = true });
    }

    [HttpGet("GetCartById/{cartId}")]
    public IActionResult GetCartById(int cartId)
    {
        var cart = _cartService.GetCartById(cartId);
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        return Ok(cart);
    }
}
