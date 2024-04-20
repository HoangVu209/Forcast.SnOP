using Forcast.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task<int> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task<int> RemoveAsync(int id);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    Task<int> UpdateAsync(T entity);
    Task<PagedList<T>> GetPagedAsync(int pageNumber, int pageSize);

}
