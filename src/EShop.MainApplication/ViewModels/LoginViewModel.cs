using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.MainApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password must be atleat 8 chars")]
        public string Password { get; set; } = null!;
        [HiddenInput]
        public string? ReturnUrl { get; set; }
    }
}
