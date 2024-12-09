using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AddTicketToCartRequest
{
    /// <summary>
    /// Gets or sets the ID of the cart to which tickets should be added.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Gets or sets the list of ticket IDs to be added to the cart.
    /// </summary>
    public required List<int> TicketId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of each ticket to be added to the cart.
    /// </summary>
    public int Quantity { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService; // Cart service dependency

    /// <summary>
    /// Initializes a new instance of the CartController.
    /// </summary>
    /// <param name="cartService">The cart service.</param>
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Adds tickets to the cart.
    /// </summary>
    /// <param name="request">The request containing cart ID, ticket IDs, and quantity.</param>
    /// <returns>An IActionResult representing the result of adding tickets to the cart.</returns>
    [HttpPost("AddTicketToCart")]
    public async Task<IActionResult> AddTicketToCart([FromBody] AddTicketToCartRequest request)
    {
        // Calls the cart service to add tickets to the cart
        var result = await _cartService.AddTicketToCart(request.CartId, request.TicketId, request.Quantity);
        if (!result)
        {
            return NotFound("Ticket not found or capacity reached.");
        }

        return Ok(new { message = "Ticket added to cart successfully.", success = true });
    }

    /// <summary>
    /// Retrieves the cart by its ID.
    /// </summary>
    /// <param name="cartId">The ID of the cart to retrieve.</param>
    /// <returns>An IActionResult representing the cart data if found, or an error message if not.</returns>
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

    /// <summary>
    /// Retrieves the cart for the current authenticated user.
    /// </summary>
    /// <returns>An IActionResult representing the cart data for the current user, or an error message if not authenticated or cart not found.</returns>
    [HttpGet("GetCartForCurrentUser")]
    public async Task<IActionResult> GetCartForCurrentUser()
    {
        var userEmail = User.Identity?.Name; // Gets the authenticated user's email from the token
        if (userEmail == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var cart = await _cartService.GetCartForCurrentUser(userEmail);
        if (cart == null)
        {
            return NotFound("Cart not found.");
        }

        return Ok(cart);
    }

    /// <summary>
    /// Removes a ticket from the cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart from which the ticket will be removed.</param>
    /// <param name="ticketId">The ID of the ticket to remove from the cart.</param>
    /// <returns>An IActionResult representing the result of removing the ticket from the cart.</returns>
    [HttpDelete("RemoveTicketFromCart")]
    public async Task<IActionResult> RemoveTicketFromCart([FromQuery] int cartId, [FromQuery] int ticketId)
    {
        // Calls the cart service to remove the ticket from the cart
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);
        if (!result)
        {
            return NotFound("Cart or ticket not found.");
        }

        return Ok(new { message = "Ticket removed from cart successfully.", success = true });
    }
}
