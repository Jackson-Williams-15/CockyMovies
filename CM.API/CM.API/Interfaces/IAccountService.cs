using CM.API.Models;

namespace CM.API.Interfaces;

// Interface for user account management. Any class implementing this must provide these methods.
public interface IAccountService
{
    // Authenticates a user with username and password. Returns user details if successful.
    Task<UserDto> Authenticate(string username, string password);

    // Registers a new user with email, username, password, and date of birth. Returns user details.
    Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth);

    // Gets a user by username. Returns the user if found, otherwise null.
    Task<User> GetUserByUsername(string username);

    // Gets a user by userId. Returns the user if found, otherwise null.
    Task<User?> GetUserById(string userId);

    // Updates a user's profile (email, username, date of birth). Returns updated user details.
    Task<UserDto> UpdateUser(string userId, UserUpdateDto updateDto);

    // Updates a user's password. Returns a success flag and message.
    Task<(bool Success, string Message)> UpdatePassword(string userId, UserPasswordDto userPasswordDto);
}
