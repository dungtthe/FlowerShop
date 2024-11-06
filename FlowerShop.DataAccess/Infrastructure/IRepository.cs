using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteByEntityAsync(T entity);

        Task<bool> DeleteByIdAsync(int id);

        Task<T?> GetSingleByIdAsync(int id);

        Task<IEnumerable<T?>> GetAllAsync();
    }
}
