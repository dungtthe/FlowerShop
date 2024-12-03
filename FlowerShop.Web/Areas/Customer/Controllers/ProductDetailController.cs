using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
    [Area("CUSTOMER")]
    public class ProductDetailController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPackagingService _packagingService;
        private readonly FlowerShopContext _context;

        public ProductDetailController(FlowerShopContext context, ICategoryService categoryService, IProductService productService, IPackagingService packagingService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _packagingService = packagingService;
            _context = context;
        }

        [Route("ProductDetail/DetailsAsync/{id}")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            try
            {
                // Lấy thông tin sản phẩm qua Service
                var product = await _productService.GetProductByIdAsync(id);
                var topSellingProducts = await _productService.GetTopSellingProductsAsync();
                
                if (product == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                // Lấy danh mục liên quan (Chỉ lấy 1 danh mục)
                var categories = await _productService.GetCategoriesByProductIdAsync(id);
                var category = categories.FirstOrDefault(); // Lấy danh mục đầu tiên

                // Lấy thông tin đóng gói qua Service
                var packaging = await _productService.GetPackagingByIdAsync(product.PackagingId);



                // Truyền dữ liệu qua ViewBag
                ViewBag.Category = category;
                ViewBag.Packaging = packaging;
                ViewBag.topSellingProducts = topSellingProducts;

                return View("ProductDetail", product);
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return RedirectToAction("NotFound", "Error");
            }
        }
    }
}
