using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
    [Area("CUSTOMER")]
    public class ProductCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductCategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [Route("category/{categoryId}")]
        public async Task<IActionResult> ProductCategoryAsync(int categoryId)
        {
            try
            {
                // Lấy thông tin danh mục
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                // Lấy danh sách sản phẩm theo danh mục
                var products = await _productService.GetProductsByCategoryAsync(categoryId);
                ViewBag.Category = category; // Gửi thông tin danh mục vào ViewBag
                ViewBag.Products = products; // Gửi danh sách sản phẩm vào ViewBag

                return View(products);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpGet]
        [Route("category/{categoryId}/sort")]
        public async Task<IActionResult> GetSortedProductsPartial(int categoryId, string? sortOrder = null)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(categoryId);
                // Kiểm tra nếu không có sản phẩm
                if (products == null || !products.Any())
                {
                    return Json(new { success = false, message = "Không có sản phẩm nào." });
                }

                // Áp dụng sắp xếp
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    products = sortOrder.ToLower() switch
                    {
                        "asc" => products.OrderBy(p => p.ProductPrices.OrderByDescending(pp => pp.StartDate).FirstOrDefault()?.Price ?? 0).ToList(),
                        "desc" => products.OrderByDescending(p => p.ProductPrices.OrderByDescending(pp => pp.StartDate).FirstOrDefault()?.Price ?? 0).ToList(),
                        _ => products
                    };
                }

                return Json(new { success = true, products = products });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình xử lý." });
            }
        }




    }
}
