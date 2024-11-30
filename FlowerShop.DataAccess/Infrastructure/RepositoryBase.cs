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
        protected DbSet<T> DbSet => dbSet;
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


        //không có kết quả nào thỏa mãn thì nó trả về empty list thay vì null
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> SingleOrDefaultWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(predicate);
        }


        public async Task<(IEnumerable<T> Items, int Total, int Remaining)> GetMultiPagingAsync(Expression<Func<T, bool>>? predicate = null, int pageIndex = 0, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            int skipCount = pageIndex * pageSize;

            IQueryable<T> query = dbSet;

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var total = await query.CountAsync();

            var remaining = Math.Max(0, total - ((pageIndex + 1) * pageSize));

            query = skipCount == 0 ?
                query.Take(pageSize) :
                query.Skip(skipCount).Take(pageSize);

            return (await query.ToListAsync(), total, remaining);
        }
        #endregion
    }
}
