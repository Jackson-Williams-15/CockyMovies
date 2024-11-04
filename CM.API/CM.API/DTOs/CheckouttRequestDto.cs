public class CheckoutRequestDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public PaymentDetailsDto PaymentDetails { get; set; }
    }