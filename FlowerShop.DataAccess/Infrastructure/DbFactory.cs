using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private readonly DbContextOptions<FlowerShopContext> options;
        private FlowerShopContext? dbContext;

        public DbFactory(DbContextOptions<FlowerShopContext> options)
        {
            this.options = options;
        }

        public FlowerShopContext Init()
        {
            return dbContext ??= new FlowerShopContext(options);
        }
    }
}
