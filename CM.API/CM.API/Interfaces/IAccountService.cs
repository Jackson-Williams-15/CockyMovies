using CM.API.Models;

namespace CM.API.Interfaces;
public interface IAccountService
{
    Task<UserDto> Authenticate(string username, string password);
    Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserById(string userId);
}
