public interface ICartService
{
    bool AddTicketsToCart(int cartId, List<int> ticketIds);
    Cart GetCartById(int cartId);
    bool RemoveTicketFromCart(int cartId, int ticketId); 
}
