using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
