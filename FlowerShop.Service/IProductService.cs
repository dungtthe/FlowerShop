using FlowerShop.Common.Template;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsForIndexAsync();
        Task<ResponeMessage> AddNewProductAsync(string title,string desc,decimal price,int quantity,int packId,List<int>categoriesId,List<Tuple<int,int>>productsItem);
        Task<Product?> FindOneByIdAsync(int id);
        Task<Product> UpdateImageAsync(Product product);
    }
}
