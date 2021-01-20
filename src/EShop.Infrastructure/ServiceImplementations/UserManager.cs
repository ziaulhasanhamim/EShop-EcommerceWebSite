using EShop.DataAccess.Contexts;
using EShop.DataAccess.Models;
using EShop.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.ServiceImplementations
{
    public class UserManager : IUserManager
    {
        DefaultDbContext _context;
        IHttpContextAccessor _httpContextAccessor;

        public UserManager(DefaultDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(User?, UserResult)> CreateUserAsync(string email, string name, string password)
        {
            email = email.ToLower();
            if (await DoesEmailExistsAsync(email))
            {
                return (null, UserResult.EmailFailure);
            }
            var user = new User { Email = email, Name = name, CreatedOn = DateTime.Now };
            SetPassword(user, password);
            var entry = await _context.AddAsync<User>(user);
            await _context.SaveChangesAsync();
            return (entry.Entity, UserResult.Succeed);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.FindAsync<User>(id);
        }

        public async Task<UserResult> SigninUserAsync(string email, string password)
        {
            email = email.ToLower();
            var httpContext = _httpContextAccessor.HttpContext;
            var user = await GetUserByEmailAsync(email);
            if (user == null)
                return UserResult.EmailFailure;
            if (!CheckPassword(user, password))
                return UserResult.PasswordFailure;
            var claims = new Claim[]
            {
                new Claim("userId", user.Id.ToString())
            };
            ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, "user");
            await httpContext!.SignInAsync("UserAuth", new ClaimsPrincipal(claimIdentity));
            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return UserResult.Succeed;
        }

        public async Task<bool> SignoutUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User.Identity?.IsAuthenticated ?? false)
            {
                await httpContext!.SignOutAsync();
                return true;
            }
            return false;
        }
        
        public async Task<bool> DoesEmailExistsAsync(string email) => await _context.Set<User>().AnyAsync(u => (email.ToLower() == u.Email));

        public async Task<User?> GetAuthenticatedUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var result = await httpContext!.AuthenticateAsync("UserAuth");
            if (!result.Succeeded)
                return null;
            var userId = int.Parse(result.Principal?.Claims.Where(c => c.Type == "userId").FirstOrDefault()?.Value ?? "0");
            return await GetUserByIdAsync(userId);
        }

        public void SetPassword(User user, string password)
        {
            using (var sha = SHA512.Create())
            {
                var byteHash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                user.Password = BitConverter.ToString(byteHash);
            }
        }

        public bool CheckPassword(User user, string password)
        {
            string hashed;
            using (var sha = SHA512.Create())
            {
                var byteHash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                hashed = BitConverter.ToString(byteHash);
            }
            return (hashed == user.Password);
        }
    }
}
