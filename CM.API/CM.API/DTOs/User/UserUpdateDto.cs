using System;
using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// This is an optional field, allowing the user to update their email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// This is an optional field, allowing the user to update their username.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the user.
    /// This is a required field for updating the user's birth date.
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}
