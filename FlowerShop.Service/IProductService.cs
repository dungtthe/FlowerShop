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
        Task<IEnumerable<Product>> GetProductsForIndexInAdminAsync();
        Task<IEnumerable<Product>> GetProductsForIndexInCustomerAsync();
        Task<ResponeMessage> AddNewProductAsync(string title,string desc,decimal price,int quantity,int packId,List<int>categoriesId,List<Tuple<int,int>>productsItem);
        Task<Product?> FindOneByIdAsync(int id, bool include = true);
        Task<Product> UpdateImageAsync(Product product);
        Task<ResponeMessage> UpdateProductAsync(Product product,int quantityNew,List<ProductPrice> productPrices,List<int> categoriesId, List<Tuple<int, int>> productsItem);
        Task<IEnumerable<Product>> GetTopSellingProductsAsync();
        Task<(IEnumerable<Product> products, int total, int remaining)> GetNewProductsAsync(int pageIndex = 0, int pageSize = 10);
        Task<(IEnumerable<Product> products, int total, int remaining)> GetGiftCategoryProductsAsync(int pageIndex = 0, int pageSize = 10);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<Product> GetProductByIdAsync(int productId);
        Task<IEnumerable<Category>> GetCategoriesByProductIdAsync(int productId);
        Task<Packaging> GetPackagingByIdAsync(int packagingId);


    }
}




















