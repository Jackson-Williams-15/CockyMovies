using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Soedetail
{
    public int Id { get; set; }

    public int? Detailid { get; set; }

    public string? Invoiceid { get; set; }

    public string? Productid { get; set; }

    public int? Qty { get; set; }

    public double? Subtotalmrc { get; set; }

    public double? Subtotalnrc { get; set; }

    public double? Subtotaltax { get; set; }
}
