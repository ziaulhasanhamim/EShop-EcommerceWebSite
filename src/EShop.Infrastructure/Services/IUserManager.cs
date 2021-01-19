using EShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Services
{
    public enum UserError
    {
        NoError,
        EmailErorr,
        NameError,
        PasswordError,
        UserNotFoundError
    }
    public interface IUserManager
    {
        public Task<(UserError, string?)> SigninUserAsyn(string email);
        public Task<(UserError, string?)> SignoutUserAsync();
        public Task<(UserError, string?)> CreateUserAsync(User user, string password);
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
    }
}
