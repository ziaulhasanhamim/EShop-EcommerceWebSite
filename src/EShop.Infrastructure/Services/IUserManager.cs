using EShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Services
{
    public interface IUserManager
    {
        public Task SigninUserAsyn(User user);
        public Task SignoutUserAsync();
        public Task CreateUserAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByEmail(string email);
    }
}
