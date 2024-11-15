using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IProductPriceService
    {
        Task<bool> AddProductPrice(int  productId, int price);
    }
}
