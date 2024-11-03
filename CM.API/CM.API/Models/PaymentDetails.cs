using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models {
public class PaymentDetails
{
    public int Id { get; set; } // Primary key

    public int PaymentDetailsId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }   
}
}
