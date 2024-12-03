using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class PackagingRepository : RepositoryBase<Packaging>, IPackagingRepository
    {
        private readonly FlowerShopContext _context;
        public PackagingRepository(FlowerShopContext context,IDbFactory dbFactory) : base(dbFactory)
        {
            _context = context;
        }

        // Thêm phương thức SingleOrDefaultAsync
        public async Task<Packaging> SingleOrDefaultAsync(Expression<Func<Packaging, bool>> predicate)
        {
            // Sử dụng _context đã được inject
            return await _context.Set<Packaging>().FirstOrDefaultAsync(predicate);
        }
    }
}
