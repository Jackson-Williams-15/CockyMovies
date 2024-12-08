using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Soedetail
{
    public int Id { get; set; }

    public int? Detailid { get; set; }

    public string? Invoiceid { get; set; }

    public string? Productid { get; set; }

    public int? Qty { get; set; }

    public float? Subtotalmrc { get; set; }

    public float? Subtotalnrc { get; set; }

    public float? Subtotaltax { get; set; }
}
