using EShop.DataAccess.Models;
using EShop.Infrastructure.Services;
using EShop.MainApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Register(string? returnUrl)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect(returnUrl ?? "/");
            }
            return View(new RegisterViewModel { ReturnUrl=returnUrl});
        }

        [HttpPost("register", Name = "Register")]
        public async Task<ActionResult> Register(RegisterViewModel userViewModel)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect(userViewModel.ReturnUrl ?? "/");
            }
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateUserAsync(userViewModel.Email, userViewModel.Name, userViewModel.Password);
                if (result.Item2 == UserResult.Succeed)
                {
                    await _userManager.SigninUserAsync(userViewModel.Email, userViewModel.Password);
                    return Redirect(userViewModel.ReturnUrl ?? "/"); ;
                }
                ModelState.AddModelError("Email", "Email Already Exists");
            }
            return View(userViewModel);
        }

        [HttpGet("login", Name = "Login")]
        public ActionResult Login(string? returnUrl)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect(returnUrl ?? "/");
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost("login/", Name = "Login")]
        public async Task<ActionResult> Login(LoginViewModel userViewModel)
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect(userViewModel.ReturnUrl ?? "/");
            }
            if (ModelState.IsValid)
            {
                var result = await _userManager.SigninUserAsync(userViewModel.Email, userViewModel.Password);
                switch(result) 
                {
                    case UserResult.Succeed:
                        return Redirect(userViewModel.ReturnUrl ?? "/");
                    case UserResult.EmailFailure:
                        ModelState.AddModelError("Email", "Wrong Email");
                        break;
                    case UserResult.PasswordFailure:
                        ModelState.AddModelError("Password", "Wrong Password");
                        break;
                }
            }
            return View(userViewModel);
        }

        [Authorize]
        [HttpGet("logout", Name = "Logout")]
        public async Task<ActionResult> Logout()
        {
            await _userManager.SignoutUserAsync();
            return RedirectToRoute("Index");
        }
    }
}
