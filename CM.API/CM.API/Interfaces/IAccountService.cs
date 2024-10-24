using CM.API.Models;

namespace CM.API.Interfaces;
    public interface IAccountService
    {
        Task<User> Authenticate(string username, string password);
    }
