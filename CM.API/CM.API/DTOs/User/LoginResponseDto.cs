public class LoginResponseDto
{
    // The authentication token returned after successful login.
    public string Token { get; set; }

    // User details returned after successful login.
    public UserDto User { get; set; }

    // The ID of the user's cart.
    public int? CartId { get; set; }
}
