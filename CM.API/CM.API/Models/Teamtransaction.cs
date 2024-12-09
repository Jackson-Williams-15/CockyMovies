using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Teamtransaction
{
    public int Id { get; set; }

    public string? Teamid { get; set; }

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

    public float? Statetx { get; set; }

    public float? Fedtax { get; set; }

    public float? Cityregiontax { get; set; }

    public float? Othertax1 { get; set; }

    public float? Othertax2 { get; set; }

    public string? Othertax1descr { get; set; }

    public string? Othertax2descr { get; set; }
}
