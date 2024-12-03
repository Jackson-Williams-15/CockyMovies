using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class TestComment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TestPost? Post { get; set; }

    public virtual TestUser? User { get; set; }
}
