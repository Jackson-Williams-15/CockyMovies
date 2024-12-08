using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Currencymark
{
    public int Id { get; set; }

    public string? Currencyid { get; set; }

    public string? Currencydescription { get; set; }

    public double? Dollarmark { get; set; }

    public double? Yuanmark { get; set; }

    public double? Sterlingmark { get; set; }
}
