using CM.API.Models;

public interface ICartService
{
    /// <summary>
    /// Adds tickets to the specified cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart where tickets will be added.</param>
    /// <param name="ticketIds">The list of ticket IDs to be added to the cart.</param>
    /// <param name="quantity">The quantity of each ticket to be added to the cart.</param>
    /// <returns>A task that represents the asynchronous operation. Returns true if the tickets were successfully added, otherwise false.</returns>
    Task<bool> AddTicketToCart(int cartId, List<int> ticketIds, int quantity);

    /// <summary>
    /// Retrieves the cart details for a specified cart ID.
    /// </summary>
    /// <param name="cartId">The ID of the cart to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. Returns the cart details as a CartDto if found, otherwise null.</returns>
    Task<CartDto?> GetCartById(int cartId);

    /// <summary>
    /// Removes a ticket from the specified cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart from which the ticket will be removed.</param>
    /// <param name="ticketId">The ID of the ticket to be removed from the cart.</param>
    /// <returns>A task that represents the asynchronous operation. Returns true if the ticket was successfully removed, otherwise false.</returns>
    Task<bool> RemoveTicketFromCart(int cartId, int ticketId);

    /// <summary>
    /// Retrieves the cart for a specific user by their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose cart is to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. Returns the user's cart as a Cart object.</returns>
    Task<Cart> GetCartByUserId(int userId);

    /// <summary>
    /// Retrieves the cart for the current user based on their email.
    /// </summary>
    /// <param name="userEmail">The email of the currently authenticated user.</param>
    /// <returns>A task that represents the asynchronous operation. Returns the user's cart as a Cart object.</returns>
    Task<Cart> GetCartForCurrentUser(string userEmail);
}
