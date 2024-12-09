using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AddTicketToCartRequest
{
    // The ID of the cart to which the ticket will be added.
    public int CartId { get; set; }

    // A list of ticket IDs to be added to the cart.
    public List<int> TicketId { get; set; }

    // The quantity of tickets to add.
    public int Quantity { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    // Constructor that injects the ICartService dependency.
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // Endpoint to add tickets to a cart.
    // Accepts the cart ID, list of ticket IDs, and quantity in the request body.
    [HttpPost("AddTicketToCart")]
    public async Task<IActionResult> AddTicketToCart([FromBody] AddTicketToCartRequest request)
    {
        // Calls the service to add tickets to the cart.
        var result = await _cartService.AddTicketToCart(request.CartId, request.TicketId, request.Quantity);
        
        // If adding tickets fails, returns a NotFound response.
        if (!result)
        {
            return NotFound("Ticket not found or capacity reached.");
        }

        // If successful, returns an OK response with a success message.
        return Ok(new { message = "Ticket added to cart successfully.", success = true });
    }

    // Endpoint to get a cart by its ID.
    [HttpGet("GetCartById/{cartId}")]
    public async Task<IActionResult> GetCartById(int cartId)
    {
        // Fetches the cart by ID from the service.
        var cartDto = await _cartService.GetCartById(cartId);

        // If the cart is not found, returns a NotFound response.
        if (cartDto == null)
        {
            return NotFound("Cart not found.");
        }

        // If found, returns the cart data in the response.
        return Ok(cartDto);
    }

    // Endpoint to get the current user's cart.
    [HttpGet("GetCartForCurrentUser")]
    public async Task<IActionResult> GetCartForCurrentUser()
    {
        // Gets the current user's email (assuming it's in the Name claim).
        var userEmail = User.Identity.Name;

        // Fetches the cart for the current user from the service.
        var cart = await _cartService.GetCartForCurrentUser(userEmail);

        // If the cart is not found, returns a NotFound response.
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        // If found, returns the user's cart data.
        return Ok(cart);
    }

    // Endpoint to remove a ticket from a cart.
    [HttpDelete("RemoveTicketFromCart")]
    public async Task<IActionResult> RemoveTicketFromCart([FromQuery] int cartId, [FromQuery] int ticketId)
    {
        // Calls the service to remove the ticket from the cart.
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);

        // If the removal fails (cart or ticket not found), returns a NotFound response.
        if (!result)
        {
            return NotFound("Cart or ticket not found.");
        }

        // If successful, returns an OK response with a success message.
        return Ok(new { message = "Ticket removed from cart successfully.", success = true });
    }
}
