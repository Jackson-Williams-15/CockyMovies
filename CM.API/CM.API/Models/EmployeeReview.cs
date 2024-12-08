using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class EmployeeReview
{
    public int Id { get; set; }

    public string? Employeefullname { get; set; }

    public string? Reviewdate { get; set; }

    public string? Reviewgivendate { get; set; }

    public string? Reviewdetails { get; set; }

    public string? Calendaryear { get; set; }

    public string? Notesondelivery { get; set; }

    public string? Reviewurl { get; set; }

    public byte[]? Reviewimage { get; set; }
}
