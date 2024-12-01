using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class TestUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<TestComment> TestComments { get; set; } = new List<TestComment>();

    public virtual ICollection<TestPost> TestPosts { get; set; } = new List<TestPost>();
}
