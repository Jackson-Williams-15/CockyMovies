using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Countryhq
{
    public int Id { get; set; }

    public int? Hqid { get; set; }

    public string? Hqaddress1 { get; set; }

    public string? Hqaddress2 { get; set; }

    public string? Hqcity { get; set; }

    public string? Hqregion { get; set; }

    public string? Hqcountry { get; set; }

    public string? Companyid { get; set; }

    public string? Subaccount { get; set; }

    public string? Storeid { get; set; }
}
