using CM.API.Models;

public class UserDto
{
    /// <summary>
    /// Gets or sets the unique ID of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// The username is a required field.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the user.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the user's cart information.
    /// </summary>
    public CartDto? Cart { get; set; }

    /// <summary>
    /// Gets or sets the user's payment details.
    /// </summary>
    public PaymentDetailsDto? PaymentDetails { get; set; }

    /// <summary>
    /// Gets or sets the role of the user (e.g., admin, customer).
    /// </summary>
    public string? Role { get; set; }
}
