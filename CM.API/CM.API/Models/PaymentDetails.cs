using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models;
public class PaymentDetails
{
    public int Id { get; set; } // Primary key
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string CVV { get; set; }
    public string CardHolderName { get; set; }
}
