using System;
using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;

// Represents a user in the application and is mapped to a database table.
public class User
{
    // Primary key for the User table in the database.
    [Key]
    public int Id { get; set; }

    // Email property for the user.
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    // Username property for the user.
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string Username { get; set; }

    // Date of Birth property for the user.
    // A custom validation ensures that the Date of Birth is not in the future.
    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]  // Specifies that this is a date field for UI rendering.
    [CustomValidation(typeof(User), nameof(ValidateDateOfBirth))]  // Custom validation to check if the date is not in the future.
    public DateTime DateOfBirth { get; set; }

    // Password property for the user.
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]  // Specifies that this is a password field for UI rendering.
    public string Password { get; set; }

    // Custom validation method for DateOfBirth to ensure the user is not born in the future.
    public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
    {
        // If the provided Date of Birth is in the future, return a validation failure.
        if (dateOfBirth > DateTime.Now)
        {
            return new ValidationResult("Date of Birth cannot be in the future.");
        }
        // If the Date of Birth is valid, return a successful validation result.
        return ValidationResult.Success;
    }

    // Navigation property representing the user's cart.
    public Cart? Cart { get; set; }

    // Navigation property representing the user's order results.
    public List<OrderResult>? OrderResults { get; set; }

    // Foreign key for PaymentDetails.
    public int? PaymentDetailsId { get; set; }

    // Navigation property to the PaymentDetails entity, which contains the user's payment information.
    public PaymentDetails? PaymentDetails { get; set; }

    public string? Role { get; set; }
}
