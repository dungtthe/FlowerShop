using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private FlowerShopContext? dbContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory { get; private set; }

        protected FlowerShopContext DbContext => dbContext ??= DbFactory.Init();//nếu context null thì khởi tạo
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }
        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }
        public bool DeleteByEntity(T entity)
        {
            dbSet.Remove(entity);
            return true;
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null) return false;

            dbSet.Remove(entity);
            return true;
        }
        public async Task<T?> GetSingleByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        #endregion
    }
}
