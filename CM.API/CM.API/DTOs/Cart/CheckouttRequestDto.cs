public class CheckoutRequestDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart for the checkout request.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user initiating the checkout process.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the checkout request is made.
    /// This helps track when the checkout request was initiated.
    /// </summary>
    public DateTime RequestDate { get; set; }

    /// <summary>
    /// Gets or sets the payment details associated with the checkout request.
    /// This includes the card information and other payment-related fields.
    /// </summary>
    public PaymentDetailsDto? PaymentDetails { get; set; }
}
