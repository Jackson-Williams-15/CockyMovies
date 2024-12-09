using System;
using System.ComponentModel.DataAnnotations;

public class UserCreateDto
{
    // Required field for the user's email, must be a valid email address.
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    // Required field for the user's username, must be between 3 and 50 characters.
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string Username { get; set; }

    // Required field for the user's date of birth, must be a valid date.
    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]  // Specifies that the value is a date.
    public DateTime DateOfBirth { get; set; }

    // Required field for the user's password, must be at least 6 characters long.
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]  // Specifies that this is a password field for secure entry.
    public string Password { get; set; }
}
