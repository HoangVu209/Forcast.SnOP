using Forcast.Data.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Forcast.Data.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedList<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await PagedList<T>.CreateAsync(_dbSet, pageNumber, pageSize);
        }
    }
}
