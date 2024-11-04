using System;
using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(User), nameof(ValidateDateOfBirth))]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
    {
        if (dateOfBirth > DateTime.Now)
        {
            return new ValidationResult("Date of Birth cannot be in the future.");
        }
        return ValidationResult.Success;
    }

    public Cart Cart { get; set; }
    public List<OrderResult> OrderResults { get; set; }
}
