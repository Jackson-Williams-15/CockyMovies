using System.Collections.Generic;
using System.Linq;

namespace CM.API.Models;
public class OrderResult
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public DateTime ProcessedDate { get; set; }
    public bool Success { get; set; }
    public string Details { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderTicket> Tickets { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}