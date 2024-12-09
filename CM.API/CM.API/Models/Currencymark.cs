using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Currencymark
{
    public int Id { get; set; }

    public string? Currencyid { get; set; }

    public string? Currencydescription { get; set; }

    public float? Dollarmark { get; set; }

    public float? Yuanmark { get; set; }

    public float? Sterlingmark { get; set; }
}
