using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IProductProductItemService
    {
        Task<bool> CheckExistPrductItem(int id);
        Task DeleteAsync(int id);
        Task<ProductProductItem> AddAsync(ProductProductItem productProductItem);

    }
}
