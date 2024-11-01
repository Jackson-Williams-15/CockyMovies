using CM.API.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetCart(int userId)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/add/{ticketId}")]
        public async Task<ActionResult> AddTicketToCart(int userId, int ticketId)
        {
            try
            {
                await _cartService.AddTicketToCartAsync(userId, ticketId);
                return Ok("Ticket added to cart");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}/remove/{ticketId}")]
        public async Task<ActionResult> RemoveTicketFromCart(int userId, int ticketId)
        {
            try
            {
                await _cartService.RemoveTicketFromCartAsync(userId, ticketId);
                return Ok("Ticket removed from cart");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
}
