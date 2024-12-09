using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Employee
{
    public int Id { get; set; }

    public string? Employeefullname { get; set; }

    public string? Firstname { get; set; }

    public string? Middle { get; set; }

    public string? Lastname { get; set; }

    public string? Companyid { get; set; }

    public string? Subaccount { get; set; }

    public int? Jid { get; set; }

    public string? Ncrid { get; set; }

    public string? Dynamicsid { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? Regionstate { get; set; }

    public string? Postalcode { get; set; }
}
