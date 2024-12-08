using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class CompanyCredittran
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public string? Cardtype { get; set; }

    public string? Cardnumber { get; set; }

    public string? Transdate { get; set; }

    public double? Transamount { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountSubaccount { get; set; }
}
