using CM.API.Models;

public interface ICartService
{
    Task<bool> AddTicketToCart(int cartId, List<int> ticketIds, int quantity);
    Task<CartDto> GetCartById(int cartId);
    Task<bool> RemoveTicketFromCart(int cartId, int ticketId);
    Task<Cart> GetCartByUserId(int userId);
    Task<Cart> GetCartForCurrentUser(string userEmail);
}
