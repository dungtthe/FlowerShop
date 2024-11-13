﻿using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IProductItemService
    {
        Task<ICollection<ProductItem>> GetProductsItemAsync();
        Task<ProductItem> UpdateAsync(ProductItem productItem);
        Task<ProductItem> GetSingleById(int id);
        Task DeleteAsync(int id);
    }
}