using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class OrderResult
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public string? ProcessedDate { get; set; }

    public bool? Success { get; set; }

    public string? Details { get; set; }

    public string? TotalPrice { get; set; }

    public int? Tiketid { get; set; }

    public int? UserId { get; set; }

    public string? Username { get; set; }
}
