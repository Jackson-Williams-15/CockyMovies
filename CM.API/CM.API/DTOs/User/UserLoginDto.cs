using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    // Required field for the user's username.
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }

    // Required field for the user's password, treated as a password type.
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]  // Ensures the password is rendered securely (e.g., in a masked field).
    public string Password { get; set; }
}
