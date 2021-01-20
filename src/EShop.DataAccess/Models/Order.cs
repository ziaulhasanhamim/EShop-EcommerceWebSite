using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateOrdered { get; set; }
        public DateTime? DateDelivered { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = null!;
    }
}
