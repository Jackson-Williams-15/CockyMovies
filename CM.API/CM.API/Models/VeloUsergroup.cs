using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsergroup
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int? Lft { get; set; }

    public int? Rgt { get; set; }

    public string? Title { get; set; }
}
