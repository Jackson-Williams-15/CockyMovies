using System.Threading.Tasks;
using CM.API.Models;

namespace CM.API.Interfaces;
    public interface IAccountService
    {
        User Authenticate(string username, string password);
    }
