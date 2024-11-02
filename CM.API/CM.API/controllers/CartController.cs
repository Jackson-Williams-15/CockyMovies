using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


public class AddTicketToCartRequest
{
    public int CartId { get; set; }
    public List<int> TicketId { get; set; }
    public int Quantity { get; set; }
}

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
    public async Task<IActionResult> AddTicketToCart([FromBody] AddTicketToCartRequest request)
    {
        var result = await _cartService.AddTicketToCart(request.CartId, request.TicketId, request.Quantity);
        if (!result)
        {
            return NotFound("Ticket not found or capacity reached.");
        }

        return Ok(new { message = "Ticket added to cart successfully.", success = true });
    }

    [HttpGet("GetCartById/{cartId}")]
    public async Task<IActionResult> GetCartById(int cartId)
    {
        var cartDto = await _cartService.GetCartById(cartId);
        if (cartDto == null)
        {
            return NotFound("Cart not found.");
        }

        return Ok(cartDto);
    }

    [HttpGet("GetCartForCurrentUser")]
    public async Task<IActionResult> GetCartForCurrentUser()
    {
        var userEmail = User.Identity.Name; // Assuming the user's email is stored in the Name claim
        var cart = await _cartService.GetCartForCurrentUser(userEmail);
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        return Ok(cart);
    }

    [HttpDelete("RemoveTicketFromCart")]
    public async Task<IActionResult> RemoveTicketFromCart(int cartId, int ticketId)
    {
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);
        if (!result)
        {
            return NotFound("Cart or ticket not found.");
        }

        return Ok(new { message = "Ticket removed from cart successfully.", success = true });
    }
}