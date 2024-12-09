public class SignUpResponseDto
{
    // User details returned after successful sign-up.
    public UserDto User { get; set; }

    // The ID of the user's cart.
    public int? CartId { get; set; }
}
