using EShop.DataAccess.Models;
using EShop.Infrastructure.Services;
using EShop.MainApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.MainApplication.Controllers
{
    [Route("account")]
    public class AccountsController : Controller
    {
        IUserManager _userManager;

        public AccountsController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("register", Name = "Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("register", Name = "Register")]
        public async Task<ActionResult> Register(RegisterViewModel userViewModel)
        {
            if (userViewModel.Password != userViewModel.ConfirmPassword)
            {
                userViewModel.Error = "Password Doesn't Match";
                return View(userViewModel);
            }
            var result = await _userManager.CreateUserAsync(new User { Name = userViewModel.Name, Email = userViewModel.Email, CreatedOn = DateTime.Now }, userViewModel.Password);
            if(result.Item1 == UserError.NoError)
            {
                return RedirectToRoute("Index");
            }
            userViewModel.Error = result.Item2;
            return View(userViewModel);
        }
    }
}
