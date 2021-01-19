using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
    }
}
