using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess;
using FlowerShop.Service.ServiceImpl;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
    [Area("CUSTOMER")]
    public class ProductCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPackagingService _packagingService;
        private readonly FlowerShopContext _context;

        public ProductCategoryController(FlowerShopContext context, ICategoryService categoryService, IProductService productService, IPackagingService packagingService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _packagingService = packagingService;
            _context = context;
        }

        [Route("category/{categoryId}")]
        public async Task<IActionResult> ProductCategoryAsync(int categoryId)
        {
            try
            {
                // Lấy thông tin danh mục
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);
                var topSellingProducts = await _productService.GetTopSellingProductsAsync();
                if (category == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                // Lấy danh sách sản phẩm theo danh mục
                var products = await _productService.GetProductsByCategoryAsync(categoryId);

                // Lấy danh sách đóng gói từ database
                var packagings = await _packagingService.GetAllPackagingAsync();

                // Gửi dữ liệu vào ViewBag
                ViewBag.Category = category; // Gửi thông tin danh mục vào ViewBag
                ViewBag.Products = products; // Gửi danh sách sản phẩm vào ViewBag
                ViewBag.Packagings = packagings; // Gửi danh sách đóng gói vào ViewBag
                ViewBag.CategoryId = categoryId;
                ViewBag.topSellingProducts = topSellingProducts;
                // Trả về view với danh sách sản phẩm
                return View(products);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Error");
            }
        }



   
    }
}
