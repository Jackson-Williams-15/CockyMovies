using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    /// <summary>
    /// Represents the result of an order processed through the checkout system.
    /// </summary>
    public class OrderResult
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// This is the primary key for the OrderResult entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cart associated with the order.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was processed.
        /// </summary>
        public DateTime ProcessedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the order was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets any additional details about the order, such as errors or notes.
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Gets or sets the total price of the order, including all tickets.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the list of tickets associated with the order.
        /// </summary>
        public List<OrderTicket>? Tickets { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who placed the order.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who placed the order.
        /// This is a navigation property to the User entity.
        /// </summary>
        public User? User { get; set; }
    }
}
