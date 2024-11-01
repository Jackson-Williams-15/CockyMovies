public interface ICartService
{
    Task<CartDto> GetCartByUserIdAsync(int userId);
    Task AddTicketToCartAsync(int userId, int ticketId);
    Task RemoveTicketFromCartAsync(int userId, int ticketId);
}