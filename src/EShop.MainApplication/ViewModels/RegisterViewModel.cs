using EShop.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.MainApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(256,MinimumLength = 8, ErrorMessage = "Password must be atleat 8 chars")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password must be atleat 8 chars")]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; } = null!;
        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
