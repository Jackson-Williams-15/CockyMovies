using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// The username is a required field.
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// The password is a required field and must be in a password format.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
