public class UserPasswordDto
{
    // The user's current password to be validated.
    public string? OldPassword { get; set; }

    // The new password that the user wants to set.
    public string? NewPassword { get; set; }
}
