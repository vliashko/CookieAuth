using CookieAuth.Application.Models;
using System.Threading.Tasks;

namespace CookieAuth.Application.Services.Contracts
{
    public interface IAuthService
    {
        Task<User> ValidateUser(string login, string password);
    }
}
