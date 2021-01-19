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
            if (await _userManager.DoesEmailExistsAsync(userViewModel.Email))
            {
                ModelState.AddModelError("Email", "Email Already Exists");
            }
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            var result = await _userManager.CreateUserAsync(new User { Name = userViewModel.Name, Email = userViewModel.Email, CreatedOn = DateTime.Now }, userViewModel.Password);
            return RedirectToRoute("Index");
        }
    }
}
