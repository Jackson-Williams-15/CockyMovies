using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models;
public class CheckoutRequest
{
    public int Id { get; set; } // Primary key
    public int CartId { get; set; }
    public int UserId { get; set; }
    public DateTime RequestDate { get; set; }
    public PaymentDetails PaymentDetails { get; set; }
}