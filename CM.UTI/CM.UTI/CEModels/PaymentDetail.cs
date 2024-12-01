using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class PaymentDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public string? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentDate { get; set; }

    public string? CardNumber { get; set; }

    public string? ExpiryDate { get; set; }

    public string? Cvv { get; set; }

    public string? CardHolderName { get; set; }
}
