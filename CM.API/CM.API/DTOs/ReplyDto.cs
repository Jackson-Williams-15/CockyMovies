/// <summary>
/// Data transfer object for a reply.
/// </summary>
public class ReplyDto
{
    /// <summary>
    /// Gets or sets the reply identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the review identifier that this reply is associated with.
    /// </summary>
    public int ReviewId { get; set; }

    /// <summary>
    /// Gets or sets the author of the reply.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the body of the reply.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the reply was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}