using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private FlowerShopContext ?dbContext;

        public FlowerShopContext Init()
        {
            return dbContext ?? (dbContext = new FlowerShopContext());
        }

    }
}
