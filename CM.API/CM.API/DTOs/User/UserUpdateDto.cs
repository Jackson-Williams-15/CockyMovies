using System;
using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    public string? Email { get; set; }

    public string? Username { get; set; }

    public DateTime DateOfBirth { get; set; }
}