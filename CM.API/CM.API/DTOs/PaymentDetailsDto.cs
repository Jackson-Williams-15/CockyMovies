public class PaymentDetailsDto
{
    /// <summary>
    /// Gets or sets the card number for the payment.
    /// </summary>
    public string? CardNumber { get; set; }

    /// <summary>
    /// Gets or sets the expiry date of the card.
    /// This is typically in the format "MM/YY".
    /// </summary>
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// Gets or sets the CVV (Card Verification Value) for the card.
    /// </summary>
    public string? CVV { get; set; }

    /// <summary>
    /// Gets or sets the cardholder's name.
    /// </summary>
    public string? CardHolderName { get; set; }

    /// <summary>
    /// Gets or sets the payment method used (e.g., "Credit Card", "Debit Card").
    /// </summary>
    public string? PaymentMethod { get; set; }
}
