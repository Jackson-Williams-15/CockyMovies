using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property for User

        // Collection of tickets in the cart
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        // DateTime for when the cart was created or updated
        public DateTime UpdatedAt { get; set; }
    }
}
