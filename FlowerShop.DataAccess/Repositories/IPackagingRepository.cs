using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories
{
    public interface IPackagingRepository : IRepository<Packaging>
    {
        Task<Packaging> SingleOrDefaultAsync(Expression<Func<Packaging, bool>> predicate);
    }
}
