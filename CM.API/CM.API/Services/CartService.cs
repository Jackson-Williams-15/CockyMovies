using CM.API.Models;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartService : ICartService
{
    private readonly AppDbContext _context; // Database context to access the cart and ticket data

    // Constructor to initialize the CartService with the database context
    public CartService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds tickets to the cart for a specific cart ID.
    /// </summary>
    /// <param name="cartId">The ID of the cart to which tickets will be added.</param>
    /// <param name="ticketIds">The list of ticket IDs to be added to the cart.</param>
    /// <param name="quantity">The quantity of each ticket to be added.</param>
    /// <returns>True if tickets were added successfully, otherwise false.</returns>
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

        // Check if there are enough tickets available in the showtime
        if (ticket.Showtime.TicketsAvailable < quantity)
        {
            Console.WriteLine($"Capacity reached for showtime ID: {ticket.Showtime.Id}. Available: {ticket.Showtime.TicketsAvailable}, Requested: {quantity}");
            return false;
        }

        // Check if the ticket already exists in the cart for the same showtime
        var existingCartTicket = cart.Tickets.FirstOrDefault(t => t.ShowtimeId == ticket.ShowtimeId);
        if (existingCartTicket != null)
        {
            // Update the quantity of the ticket if it already exists
            existingCartTicket.Quantity += quantity;
        }
        else
        {
            // Add the new ticket to the cart
            cart.Tickets.Add(ticket);
            ticket.Quantity = quantity;
        }

        // Decrease the available tickets in the showtime
        ticket.Showtime.TicketsAvailable -= quantity;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves the cart details by its ID.
    /// </summary>
    /// <param name="cartId">The ID of the cart to retrieve.</param>
    /// <returns>A CartDto containing the cart details or null if not found.</returns>
    public async Task<CartDto?> GetCartById(int cartId)
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
                ShowtimeId = t.ShowtimeId,
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
                        Rating = t.Showtime.Movie?.Rating?.ToString() ?? string.Empty
                    } : null
                } : null
            }).ToList()
        };
    }

    /// <summary>
    /// Retrieves the cart for a specific user based on their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose cart is to be retrieved.</param>
    /// <returns>The user's cart if found, otherwise null.</returns>
    public async Task<Cart> GetCartByUserId(int userId)
    {
        return await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.UserId == userId);
    }

    /// <summary>
    /// Retrieves the cart for the currently authenticated user based on their email.
    /// If no cart exists, a new one is created.
    /// </summary>
    /// <param name="userEmail">The email of the currently authenticated user.</param>
    /// <returns>The user's cart.</returns>
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

    /// <summary>
    /// Removes a ticket from the specified cart.
    /// </summary>
    /// <param name="cartId">The ID of the cart from which the ticket will be removed.</param>
    /// <param name="ticketId">The ID of the ticket to be removed from the cart.</param>
    /// <returns>True if the ticket was successfully removed, otherwise false.</returns>
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

        // If there is more than one ticket, just decrease the quantity
        if (ticket.Quantity > 1)
        {
            ticket.Quantity -= 1;
            ticket.Showtime.TicketsAvailable += 1;
        }
        else
        {
            cart.Tickets.Remove(ticket);
            ticket.Showtime.TicketsAvailable += ticket.Quantity;
        }

        Console.WriteLine($"Ticket removed from cart. Showtime ID: {ticket.Showtime.Id}, TicketsAvailable: {ticket.Showtime.TicketsAvailable}");
        await _context.SaveChangesAsync();
        return true;
    }
}
