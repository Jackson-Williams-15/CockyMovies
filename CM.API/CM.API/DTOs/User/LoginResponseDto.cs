/// <summary>
/// Data Transfer Object (DTO) for the response returned after a successful user login.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Gets or sets the JWT token that is issued upon successful login.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Gets or sets the user details associated with the login.
    /// </summary>
    public UserDto? User { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user's cart.
    /// </summary>
    public int? CartId { get; set; }
}
