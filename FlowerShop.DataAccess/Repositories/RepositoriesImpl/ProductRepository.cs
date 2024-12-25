using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly FlowerShopContext _context;
        public ProductRepository(FlowerShopContext context, IDbFactory dbFactory) : base(dbFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // Lấy danh sách sản phẩm thuộc CategoryId, bao gồm cả PackagingId
            var products = await DbContext.ProductCategories
                .Where(pc => pc.CategoryId == categoryId && !pc.IsDelete) // Lọc theo CategoryId và không bị xóa
                .Include(pc => pc.Product) // Bao gồm thông tin sản phẩm
                    .ThenInclude(p => p.ProductPrices) // Bao gồm thông tin giá của sản phẩm
                .Select(pc => new Product
                {
                    Id = pc.Product.Id,
                    Title = pc.Product.Title,
                    Description = pc.Product.Description,
                    Images = pc.Product.Images,
                    PackagingId = pc.Product.PackagingId, // Bao gồm PackagingId
                    IsDelete = pc.Product.IsDelete,
                    Quantity = pc.Product.Quantity,
                    ProductPrices = pc.Product.ProductPrices
                })
                .Where(p => !p.IsDelete) // Lọc sản phẩm chưa bị xóa
                .ToListAsync(); // Thực hiện bất đồng bộ

            return products;
        }


        public async Task<decimal> GetPriceByNameAsync(string productName)
        {
            try
            {
                var productPrice = await DbContext.ProductPrices
                    .Where(pp =>
                        pp.Product.Title.ToLower() == productName.ToLower() && // So khớp tên sản phẩm (chuyển thành chữ thường)
                        !pp.Product.IsDelete && // Sản phẩm không bị xóa
                        !pp.IsDelete) // Giá không bị xóa
                    .OrderBy(pp => pp.Priority) // Sắp xếp theo độ ưu tiên
                    .FirstOrDefaultAsync(); // Lấy giá trị đầu tiên hoặc null nếu không có

                if (productPrice != null)
                {
                    Console.WriteLine($"ProductPrice: {productPrice.Price}");
                   
                }
                else
                {
                    Console.WriteLine("No price found.");
                }


                return productPrice.Price;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }




        




    }
}
