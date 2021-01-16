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
        public DbSet<Product>? Products { get; set; }

        public DefaultDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
