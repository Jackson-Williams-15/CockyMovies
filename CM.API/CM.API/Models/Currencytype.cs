using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Currencytype
{
    public int Id { get; set; }

    public string? Currencyid { get; set; }

    public string? Currencydescription { get; set; }

    public string? Exchangecorpid { get; set; }

    public string? Exchangecorpsubid { get; set; }
}
