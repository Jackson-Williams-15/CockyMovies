using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Indivtransaction
{
    public int Id { get; set; }

    public string? Custid { get; set; }

    public string? Currencyid { get; set; }

    public string? Transamount { get; set; }

    public string? Transdate { get; set; }

    public string? Transcomment1 { get; set; }

    public string? Transcomment2 { get; set; }

    public string? Branchmgrcomment { get; set; }

    public string? Erpsystemid { get; set; }

    public string? Erptransid { get; set; }

    public string? Companyid { get; set; }

    public string? Subaccount { get; set; }

    public int? Adultqty { get; set; }

    public int? Childrenqty { get; set; }

    public int? Seniorqty { get; set; }

    public double? Statetx { get; set; }

    public double? Fedtax { get; set; }

    public double? Cityregiontax { get; set; }

    public double? Othertax1 { get; set; }

    public double? Othertax2 { get; set; }

    public string? Othertax1descr { get; set; }

    public string? Othertax2descr { get; set; }
}
