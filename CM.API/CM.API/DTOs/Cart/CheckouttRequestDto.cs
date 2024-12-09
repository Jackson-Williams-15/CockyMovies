public class CheckoutRequestDto
{
    // The unique ID of the cart being checked out.
    public int CartId { get; set; }

    // The unique ID of the user making the checkout request.
    public int UserId { get; set; }

    // The date and time when the checkout request was made.
    public DateTime RequestDate { get; set; }

    // The payment details for completing the checkout.
    public PaymentDetailsDto PaymentDetails { get; set; }
}
