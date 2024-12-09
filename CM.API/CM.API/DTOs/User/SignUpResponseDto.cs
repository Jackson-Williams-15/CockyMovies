/// <summary>
/// Data Transfer Object (DTO) for the response returned after a successful user signup.
/// </summary>
public class SignUpResponseDto
{
    /// <summary>
    /// Gets or sets the user details associated with the signup.
    /// </summary>
    public UserDto? User { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user's cart.
    /// </summary>
    public int? CartId { get; set; }
}
