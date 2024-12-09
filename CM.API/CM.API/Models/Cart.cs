using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    /// <summary>
    /// Represents a shopping cart associated with a user.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart.
        /// This is the primary key for the Cart entity.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who owns the cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the cart.
        /// This is a navigation property to the User entity.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the list of tickets in the cart.
        /// This is a collection of Ticket objects that are part of the cart.
        /// </summary>
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        /// <summary>
        /// Gets the total price of all tickets in the cart.
        /// This is calculated by summing the price of each ticket.
        /// </summary>
        public decimal TotalPrice => Tickets.Sum(ticket => ticket.Price);

        /// <summary>
        /// Removes a ticket from the cart by its ID.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to remove.</param>
        /// <returns>True if the ticket was removed successfully, otherwise false.</returns>
        public bool RemoveTicket(int ticketId)
        {
            var ticketToRemove = Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            if (ticketToRemove != null)
            {
                Tickets.Remove(ticketToRemove);
                return true;
            }
            return false; // Ticket not found
        }
    }
}
