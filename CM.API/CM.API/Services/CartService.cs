using CM.API.Data;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        // Get cart by user ID
        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new Exception("Cart not found");

            return new CartDto
            {
                UserId = cart.UserId,
                Tickets = cart.Tickets.Select(t => new TicketDto
                {
                    Id = t.Id,
                    Price = t.Price,
                    ShowtimeId = t.ShowtimeId
                }).ToList(),
                UpdatedAt = cart.UpdatedAt
            };
        }

        // Add a ticket to the cart
        public async Task AddTicketToCartAsync(int userId, int ticketId)
        {
            var cart = await _context.Carts
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket not found");

            if (cart == null)
            {
                cart = new Cart { UserId = userId, Tickets = new List<Ticket>(), UpdatedAt = DateTime.UtcNow };
                _context.Carts.Add(cart);
            }

            cart.Tickets.Add(ticket);
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        // Remove a ticket from the cart
        public async Task RemoveTicketFromCartAsync(int userId, int ticketId)
        {
            var cart = await _context.Carts
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) throw new Exception("Cart not found");

            var ticket = cart.Tickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null) throw new Exception("Ticket not found in cart");

            cart.Tickets.Remove(ticket);
            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }