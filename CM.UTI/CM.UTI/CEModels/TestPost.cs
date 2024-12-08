using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class TestPost
{
    public int PostId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<TestComment> TestComments { get; set; } = new List<TestComment>();

    public virtual TestUser? User { get; set; }
}
