using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Soesummary
{
    public int Id { get; set; }

    public string? Invoiceid { get; set; }

    public float? Soemrctotal { get; set; }

    public float? Soenrctotal { get; set; }

    public string? Companyid { get; set; }

    public string? Subaccount { get; set; }

    public string? Purhcaseordernum { get; set; }

    public string? Paytype { get; set; }

    public string? Checkid { get; set; }

    public string? Stripetransid { get; set; }

    public string? Paypaltransid { get; set; }

    public string? Creditdebit { get; set; }
}
