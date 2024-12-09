using CM.API.Models;
public class UserDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public required string Username { get; set; }
    public DateTime DateOfBirth { get; set; }
    public CartDto? Cart { get; set; }
    public PaymentDetailsDto? PaymentDetails { get; set; }
    public string? Role { get; set; }
}