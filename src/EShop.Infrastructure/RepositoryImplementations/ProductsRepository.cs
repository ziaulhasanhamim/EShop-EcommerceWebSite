using EShop.DataAccess.Models;
using EShop.DataAccess.Contexts;
using EShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.RepositoryImplementations
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(DefaultDbContext context)
        {
            _context = context;
        }
    }
}
