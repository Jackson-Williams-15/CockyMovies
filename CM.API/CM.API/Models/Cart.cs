using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    
        // public string OrderStatus { get; set; } = "Pending"; // e.g., Pending, Completed


        // List of tickets in the cart
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // The total price of all tickets in the cart
        public decimal TotalPrice => Tickets.Sum(ticket => ticket.Price);

        // Removes ticket by ID
        public bool RemoveTicket(int ticketId)
        {
            var ticketToRemove = Tickets.FirstOrDefault(ticket => ticket.Id == ticketId);
            if (ticketToRemove != null)
            {
                Tickets.Remove(ticketToRemove);
                return true;
            }
            return false;
        }
    }
}
