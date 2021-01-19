using EShop.Infrastructure.Services;
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
        public string Email { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
        public string? Error { get; set; }
    }
}
