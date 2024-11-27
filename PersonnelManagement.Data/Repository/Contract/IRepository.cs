using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Repository.Contract
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<IEnumerable<T>> GetFilteredAsync(List<Expression<Func<T, bool>>> Filter, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string Properties = "");
        Task<T> FindAsync(object Id);
        Task<T> UpdateAsync(T entity);
        Task CreateAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task DeleteAsync(object Id);
        Task SaveAsync();
    }
}
