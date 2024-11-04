using CM.API.Data;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddTicketToCart(int cartId, List<int> ticketIds, int quantity)
    {
        var cart = await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.CartId == cartId);
        if (cart == null)
        {
            Console.WriteLine($"Cart with ID {cartId} not found.");
            return false;
        }

        var ticketId = ticketIds.FirstOrDefault();
        var ticket = await _context.Ticket.Include(t => t.Showtime)
                                          .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
        {
            Console.WriteLine($"Ticket with ID {ticketId} not found.");
            return false;
        }

        if (ticket.Showtime.TicketsAvailable < quantity)
        {
            Console.WriteLine($"Capacity reached for showtime ID: {ticket.Showtime.Id}. Available: {ticket.Showtime.TicketsAvailable}, Requested: {quantity}");
            return false;
        }

        // Check if ticket already exists in cart for the same showtime
        var existingCartTicket = cart.Tickets.FirstOrDefault(t => t.ShowtimeId == ticket.ShowtimeId);

        if (existingCartTicket != null)
        {
            // Update the quantity of ticket
            existingCartTicket.Quantity += quantity;
        }
        else
        {
            //New ticket entry
            cart.Tickets.Add(new Ticket
            {
                Price = ticket.Price,
                ShowtimeId = ticket.ShowtimeId,
                Quantity = quantity,
                Showtime = ticket.Showtime
            });
        }

        // Lower the available tickets
        ticket.Showtime.TicketsAvailable -= quantity;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CartDto> GetCartById(int cartId)
    {
        var cart = await _context.Carts
            .Include(c => c.Tickets)
            .ThenInclude(t => t.Showtime)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(c => c.CartId == cartId);

        if (cart == null)
        {
            return null;
        }

        return new CartDto
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            Tickets = cart.Tickets.Select(t => new CartTicketDto
            {
                Id = t.Id,
                Price = t.Price,
                Quantity = t.Quantity,
                Showtime = t.Showtime != null ? new ShowtimeDto
                {
                    Id = t.Showtime.Id,
                    StartTime = t.Showtime.StartTime,
                    Movie = t.Showtime.Movie != null ? new MovieDto
                    {
                        Id = t.Showtime.Movie.Id,
                        Title = t.Showtime.Movie.Title,
                        Description = t.Showtime.Movie.Description,
                        DateReleased = t.Showtime.Movie.DateReleased,
                        Rating = t.Showtime.Movie.Rating != null ? t.Showtime.Movie.Rating.ToString() : string.Empty // Handle null Rating
                    } : null
                } : null
            }).ToList()
        };
    }

    public async Task<Cart> GetCartByUserId(int userId)
    {
        return await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart> GetCartForCurrentUser(string userEmail)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null)
        {
            return null;
        }

        var cart = await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.UserId == user.Id);
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = user.Id,
                User = user
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<bool> RemoveTicketFromCart(int cartId, int ticketId)
    {
        var cart = await _context.Carts
            .Include(c => c.Tickets)
            .ThenInclude(t => t.Showtime)
            .FirstOrDefaultAsync(c => c.CartId == cartId);

        if (cart == null)
        {
            Console.WriteLine($"Cart with ID {cartId} not found.");
            return false; // Cart not found
        }

        var ticket = cart.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            Console.WriteLine($"Ticket with ID {ticketId} not found in cart.");
            return false; // Ticket not found
        }

        if (ticket.Showtime == null)
        {
            Console.WriteLine($"Showtime for ticket with ID {ticketId} is null.");
            return false;
        }

        if (ticket.Quantity > 1)
        {
            ticket.Quantity -= 1;
            ticket.Showtime.TicketsAvailable += 1;
        }
        else
        {
            cart.Tickets.Remove(ticket);
            // Adjust the available tickets based on the quantity
            ticket.Showtime.TicketsAvailable += ticket.Quantity;
        }

        Console.WriteLine($"Ticket removed from cart. Showtime ID: {ticket.Showtime.Id}, TicketsAvailable: {ticket.Showtime.TicketsAvailable}");
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<OrderResult> CheckoutAsync(int cartId, PaymentDetails paymentDetails)
{
    // Step 1: Retrieve the cart
    var cart = await _context.Carts
        .Include(c => c.Tickets)
        .FirstOrDefaultAsync(c => c.CartId == cartId);
    
    if (cart == null || !cart.Tickets.Any())
    {
        return new OrderResult { Success = false, Details = "Cart is empty or not found." };
    }


    // Step 4: Update cart status and finalize order
    // cart.OrderStatus = "Completed"; // You can set this to an enum or constant if needed
    await _context.SaveChangesAsync(); // Save the updated cart status

    return new OrderResult { Success = true, Details = "Checkout successful.", OrderId = cart.CartId };
}

}