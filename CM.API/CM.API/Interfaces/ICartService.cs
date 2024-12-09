using CM.API.Models;

public interface ICartService
{
    // Adds one or more tickets to the cart for a given cart id.
    // Returns a Task boolean indicating success or failure.
    Task<bool> AddTicketToCart(int cartId, List<int> ticketIds, int quantity);

    // Retrieves a cart by its unique id.
    Task<CartDto> GetCartById(int cartId);

    // Removes a specific ticket from the cart based on the `cartId` and `ticketId`.
    // Returns a Task boolean indicating success or failure.
    Task<bool> RemoveTicketFromCart(int cartId, int ticketId);

    // Retrieves the cart associated with a specific user based on their userId.
    // Returns a Task that resolves to the Cart object for the given userId.
    Task<Cart> GetCartByUserId(int userId);

    // Retrieves the cart for the current user based on their email address.
    Task<Cart> GetCartForCurrentUser(string userEmail);
}
