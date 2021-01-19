using EShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Services
{
    public interface IUserManager
    {
        public Task<bool> SigninUserAsync(string email);
        public Task<bool> SignoutUserAsync();
        public Task<User> CreateUserAsync(User user, string password);
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<bool> DoesEmailExistsAsync(string email);
    }
}
