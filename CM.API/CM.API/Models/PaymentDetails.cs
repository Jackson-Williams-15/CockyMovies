using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models
{
    /// <summary>
    /// Represents the payment details for an order.
    /// </summary>
    public class PaymentDetails
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment details.
        /// This is the primary key for the PaymentDetails entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the order associated with this payment.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the amount paid for the order.
        /// This is the total price of the order as paid by the user.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment method used (e.g., "Credit Card", "Debit Card").
        /// </summary>
        public string? PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment was made.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the card number used for the payment.
        /// </summary>
        public string? CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the card used for the payment.
        /// Typically in the format "MM/YY".
        /// </summary>
        public string? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the CVV (Card Verification Value) for the card.
        /// This is the 3-4 digit security code on the card.
        /// </summary>
        public string? CVV { get; set; }

        /// <summary>
        /// Gets or sets the cardholder's name as it appears on the card.
        /// </summary>
        public string? CardHolderName { get; set; }
    }
}
