using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class PackagingRepository : RepositoryBase<Packaging>, IPackagingRepository
    {
        public PackagingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
