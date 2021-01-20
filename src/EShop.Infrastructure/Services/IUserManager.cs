using EShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Services
{
    public enum UserResult
    {
        Succeed,
        IdFailure,
        EmailFailure,
        PasswordFailure
    }
    public interface IUserManager
    {
        public Task<UserResult> SigninUserAsync(string email, string password);
        public Task<bool> SignoutUserAsync();
        public Task<(User?, UserResult)> CreateUserAsync(string email, string name, string password);
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<bool> DoesEmailExistsAsync(string email);
        public Task<User?> GetAuthenticatedUserAsync();
    }
}
