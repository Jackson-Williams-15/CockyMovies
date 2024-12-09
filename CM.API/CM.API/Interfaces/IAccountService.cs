using CM.API.Models;

namespace CM.API.Interfaces
{
    /// <summary>
    /// Defines the operations available for managing user accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Authenticates a user based on their username and password.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user's details if authentication is successful, otherwise null.</returns>
        Task<UserDto?> Authenticate(string username, string password);

        /// <summary>
        /// Registers a new user with the provided details.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="username">The user's chosen username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="dateOfBirth">The user's date of birth.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the newly created user's details.</returns>
        Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found, otherwise null.</returns>
        Task<User> GetUserByUsername(string username);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found, otherwise null.</returns>
        Task<User?> GetUserById(string userId);

        /// <summary>
        /// Updates a user's details.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <param name="updateDto">The DTO containing the updated user details.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated user's details, or null if the update fails.</returns>
        Task<UserDto?> UpdateUser(string userId, UserUpdateDto updateDto);

        /// <summary>
        /// Updates a user's password.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <param name="userPasswordDto">The DTO containing the old and new passwords.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a success flag and message indicating the result of the password update.</returns>
        Task<(bool Success, string Message)> UpdatePassword(string userId, UserPasswordDto userPasswordDto);
    }
}
