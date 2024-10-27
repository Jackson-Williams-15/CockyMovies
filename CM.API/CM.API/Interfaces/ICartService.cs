public interface ICartService
{
    bool AddTicketToCart(int cartId, int ticketId, int quantity);
    Cart GetCartById(int cartId);
}
