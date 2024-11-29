using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        T Update(T entity);

        bool DeleteByEntity(T entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<T?> GetSingleByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T?> SingleOrDefaultWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<(IEnumerable<T> Items, int Total, int Remaining)> GetMultiPagingAsync(Expression<Func<T, bool>>? predicate = null, int pageIndex = 0, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
    }
}
