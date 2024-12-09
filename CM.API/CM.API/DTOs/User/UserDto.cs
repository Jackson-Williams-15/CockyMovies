using CM.API.Models;

public class UserDto
{
    // User's unique identifier.
    public int Id { get; set; }

    // User's email address.
    public string Email { get; set; }

    // User's username.
    public string Username { get; set; }

    // User's date of birth.
    public DateTime DateOfBirth { get; set; }

    // User's shopping cart details.
    public CartDto Cart { get; set; }

    // User's payment details (optional).
    public PaymentDetailsDto? PaymentDetails { get; set; }
    public string? Role { get; set; }
}
