using System;
using System.ComponentModel.DataAnnotations;

public class UserCreateDto
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// The email must be valid and is a required field.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// The username must be between 3 and 50 characters long and is required.
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the user.
    /// The date of birth is a required field.
    /// </summary>
    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the password for the user.
    /// The password must be at least 6 characters long and is a required field.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
