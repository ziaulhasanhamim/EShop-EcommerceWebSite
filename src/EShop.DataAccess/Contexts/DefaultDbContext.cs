using EShop.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataAccess.Contexts
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public DefaultDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(p => p.Email)
                    .IsUnique(true);
            });
            modelBuilder.Entity<Product>(entity => { });
            modelBuilder.Entity<ProductImage>(entity => { });
            modelBuilder.Entity<OrderItem>(entity => { });
            modelBuilder.Entity<Order>(entity => { });
        }
    }
}
