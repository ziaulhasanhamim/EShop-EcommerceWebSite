using EShop.DataAccess.Contexts;
using EShop.DataAccess.Models;
using EShop.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(UserError, string?)> CreateUserAsync(User user, string password)
        {
            var emailExisted = await _context.Set<User>().AnyAsync(u => (user.Email == u.Email));
            if (emailExisted)
                return (UserError.EmailErorr, "Email Already Exists");
            if (password.Length < 8)
                return (UserError.PasswordError, "Password must be atleast 8 chars");
            user.SetPassword(password);
            await _context.AddAsync<User>(user);
            await _context.SaveChangesAsync();
            return (UserError.NoError, null);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.FindAsync<User>(id);
        }

        public async Task<(UserError, string?)> SigninUserAsyn(string email)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = await _context.Set<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
                return (UserError.UserNotFoundError, "No User Found By The Email");
            var claims = new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.Name)
            };
            ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, "user");
            await httpContext!.SignInAsync("UserAuth", new ClaimsPrincipal(claimIdentity));
            return (UserError.NoError, null);
        }

        public async Task<(UserError, string?)> SignoutUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User.Identity?.IsAuthenticated ?? false)
            {
                await httpContext!.SignOutAsync();
                return (UserError.NoError, null);
            }
            return (UserError.UserNotFoundError, "User is not Authenticated");
        }
    }
}
