using EShop.DataAccess.Models;
using EShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.RepositoryImplementations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _context = null!;
        public virtual async Task<IEnumerable<T>> GetAllRowsAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetRowById(int id)
        {
            return await _context.FindAsync<T>(id);
        }
    }
}
