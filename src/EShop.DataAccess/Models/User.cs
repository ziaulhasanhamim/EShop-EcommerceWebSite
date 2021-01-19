using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLogin { get; set; }

        public void SetPassword(string password)
        {
            using (var sha = SHA512.Create())
            {
                var byteHash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                Password = BitConverter.ToString(byteHash);
            }
        }

        public bool CheckPassword(string password)
        {
            string? hashed;
            using (var sha = SHA512.Create())
            {
                var byteHash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                hashed = BitConverter.ToString(byteHash);
            }
            return (hashed == Password);
        }
    }
}
