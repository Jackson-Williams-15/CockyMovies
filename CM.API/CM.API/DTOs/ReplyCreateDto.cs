/// <summary>
/// Data transfer object for creating a reply.
/// </summary>
public class ReplyCreateDto
{
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
}