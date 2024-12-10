using System;
using System.ComponentModel.DataAnnotations;

namespace CM.API.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique ID of the user.
        /// This is the primary key for the User entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// This field is required and must be a valid email format.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// This field is required and must be between 3 and 50 characters in length.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// This field is required and must be a valid date.
        /// A custom validation ensures the date of birth cannot be in the future.
        /// </summary>
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(ValidateDateOfBirth))]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// This field is required and must be at least 6 characters long.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Custom validation method to ensure the date of birth is not in the future.
        /// </summary>
        /// <param name="dateOfBirth">The user's date of birth to validate.</param>
        /// <param name="context">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth > DateTime.Now)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }
            return ValidationResult.Success!;
        }

        /// <summary>
        /// Gets or sets the cart associated with the user.
        /// This is a navigation property to the Cart entity.
        /// </summary>
        public Cart? Cart { get; set; }

        /// <summary>
        /// Gets or sets the list of orders associated with the user.
        /// This is a navigation property to the OrderResult entity.
        /// </summary>
        public List<OrderResult>? OrderResults { get; set; }

        /// <summary>
        /// Gets or sets the payment details ID of the user.
        /// This is a foreign key to the PaymentDetails entity.
        /// </summary>
        public int? PaymentDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the payment details associated with the user.
        /// This is a navigation property to the PaymentDetails entity.
        /// </summary>
        public PaymentDetails? PaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets the role of the user (e.g., "user", "manager").
        /// This is an optional field.
        /// </summary>
        public string? Role { get; set; }
    }
}
