using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IProductItemService
    {
        Task<ICollection<ProductItem>> GetProductsAsync();
        ProductItem Update(ProductItem productItem);
    }
}
