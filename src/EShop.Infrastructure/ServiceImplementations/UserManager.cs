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

        public async Task<User> CreateUserAsync(User user, string password)
        {
            user.SetPassword(password);
            user.Email = user.Email.ToLower();
            var entry = await _context.AddAsync<User>(user);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.FindAsync<User>(id);
        }

        public async Task<bool> SigninUserAsync(string email)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = await _context.Set<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
                return false;
            var claims = new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
            };
            ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, "user");
            await httpContext!.SignInAsync("UserAuth", new ClaimsPrincipal(claimIdentity));
            return true;
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
        
        public async Task<bool> DoesEmailExistsAsync(string email) => await _context.Set<User>().AnyAsync(u => (email == u.Email));
    }
}
