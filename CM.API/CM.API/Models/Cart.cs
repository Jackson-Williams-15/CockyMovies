using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    // Represents a shopping cart for a user.
    public class Cart
    {
        // Unique identifier for the cart.
        public int CartId { get; set; }

        // Unique identifier for the user who owns the cart.
        public int UserId { get; set; }

        // The user associated with the cart.
        public User User { get; set; }

        // Collection of tickets in the cart. Initialized as an empty list.
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Calculates the total price of all tickets in the cart.
        public decimal TotalPrice => Tickets.Sum(ticket => ticket.Price);

        // Removes a ticket from the cart by its unique ticket id.
        public bool RemoveTicket(int ticketId)
        {
            // Finds the first ticket in the collection with a matching id.
            var ticketToRemove = Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            
            // If the ticket is found, remove it from the collection and return true to show success.
            if (ticketToRemove != null)
            {
                Tickets.Remove(ticketToRemove);
                return true;
            }

            // If the ticket is not found, return false to show failure.
            return false;
        }
    }
}
