public class UserPasswordDto
{
    /// <summary>
    /// Gets or sets the user's old password.
    /// This is used when changing the password to verify the current password.
    /// </summary>
    public string? OldPassword { get; set; }

    /// <summary>
    /// Gets or sets the user's new password.
    /// This is the password the user wants to set.
    /// </summary>
    public string? NewPassword { get; set; }
}
