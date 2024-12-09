using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUserUsergroupMap
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? GroupId { get; set; }
}
