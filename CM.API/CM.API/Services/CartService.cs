using CM.API.Models;

public class CartService : ICartService
{
    private readonly List<Cart> _carts;
    private readonly List<Showtime> _showtimes;

    public CartService(List<Cart> carts, List<Showtime> showtimes)
    {
        _carts = carts;
        _showtimes = showtimes;
    }

    // Add tickets directly to the cart
    public bool AddTicketsToCart(int cartId, List<int> ticketIds)
    {
        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            return false;
        }

        foreach (var ticketId in ticketIds)
        {
            var ticket = _showtimes.SelectMany(s => s.Tickets).FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null || ticket.Showtime.Tickets.Count >= ticket.Showtime.Capacity)
            {
                return false; // Cannot add ticket if capacity is reached or ticket not found
            }

            cart.Tickets.Add(ticket);
        }

        return true;
    }

    // Retrieve the cart by its ID
    public Cart GetCartById(int cartId)
    {
        return _carts.FirstOrDefault(c => c.CartId == cartId);
    }

    // Removes ticket from the cart
    public bool RemoveTicketFromCart(int cartId, int ticketId)
    {
        var cart = _carts.FirstOrDefault(c => c.CartId == cartId);
        if (cart == null)
        {
            return false; // Cart not found
        }

        var ticket = cart.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return false; // Ticket not found
        }

        cart.Tickets.Remove(ticket);
        return true;
    }
}
