using CookieAuth.Application.Data;
using CookieAuth.Application.Models;
using CookieAuth.Application.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> ValidateUser(string login, string password)
        {
            var dbUser = await _context.Users
                .Where(user => user.Login == login && user.Password == password)
                .FirstOrDefaultAsync();

            return dbUser;
        }
    }
}
