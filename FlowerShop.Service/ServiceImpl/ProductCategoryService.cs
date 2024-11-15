using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class ProductCategoryService : IProductCategoryService
    {
        public Task<ProductCategory> FindOneById(int id)
        {
            return null;
        }
    }
}
