using System;
using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    // Email field for updating the user's email.
    public string? Email { get; set; }

    // Username field for updating the user's username.
    public string? Username { get; set; }

    // Required field for updating the user's date of birth.
    public DateTime DateOfBirth { get; set; }
}
