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

    public async Task<bool> AddTicketToCart(int cartId, List<int> ticketId, int quantity)
    {
        var cart = await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.CartId == cartId);
        if (cart == null)
        {
            return false;
        }

        var ticket = await _context.Ticket.Include(t => t.Showtime).ThenInclude(s => s.Movie).FirstOrDefaultAsync(t => ticketId.Contains(t.Id));
        if (ticket == null || ticket.Showtime.Tickets.Count + quantity > ticket.Showtime.Capacity)
        {
            return false; // Cannot add ticket if capacity is reached or ticket not found
        }

        for (int i = 0; i < quantity; i++)
        {
            cart.Tickets.Add(ticket);
        }

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
        var cart = await _context.Carts.Include(c => c.Tickets).FirstOrDefaultAsync(c => c.CartId == cartId);
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
        await _context.SaveChangesAsync();
        return true;
    }
}