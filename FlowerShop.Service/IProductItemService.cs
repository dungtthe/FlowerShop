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
        Task<IEnumerable<ProductItem>> GetProductsItemAsync();
        Task<ProductItem> UpdateAsync(ProductItem productItem);
        Task<ProductItem> FindOneWithIncludeByIdAsync(int id);
        Task DeleteAsync(int id);

        Task<ProductItem> FindOneWithoutIncludeByIdAsync(int id);
    }
}
