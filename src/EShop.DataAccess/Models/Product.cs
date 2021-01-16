using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }
    }
}
