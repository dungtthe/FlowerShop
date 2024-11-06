using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class CartDetailRepository : RepositoryBase<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
