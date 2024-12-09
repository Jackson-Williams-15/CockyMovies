using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class CompanyCreditcard
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public string? Cardtype { get; set; }

    public string? Cardnumber { get; set; }

    public string? Expdate { get; set; }

    public string? Pincode { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountSubaccount { get; set; }
}
