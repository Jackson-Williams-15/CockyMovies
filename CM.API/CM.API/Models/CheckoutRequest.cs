using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    /// <summary>
    /// Represents a checkout request in the system.
    /// </summary>
    public class CheckoutRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the checkout request.
        /// This is the primary key for the checkout request.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cart associated with the checkout request.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is making the checkout request.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the checkout request was made.
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the payment details associated with the checkout request.
        /// </summary>
        public PaymentDetails? PaymentDetails { get; set; }
    }
}
