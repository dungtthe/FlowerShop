using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers
{


    [Area("CUSTOMER")]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(IAppUserService appUserService, ICategoryService categoryService, IProductService productService)
        {
            _appUserService = appUserService;
            _categoryService = categoryService;
            _productService = productService;
        }


        [Route("")]
        [Route("home")]
        public async Task<IActionResult> IndexAsync()
        {
            var topSellingProducts = await _productService.GetTopSellingProductsAsync();
            var newProducts = (await _productService.GetNewProductsAsync(0, 10)).products;
            var giftProducts = (await _productService.GetGiftCategoryProductsAsync(0, 10)).products;
            ViewBag.topSellingProducts = topSellingProducts;
            ViewBag.newProducts = newProducts;
            ViewBag.giftProducts = giftProducts;


            return View();

            //try
            //{
            //    bool result = await _appUserService.LoginAsync("1", "1", false);
            //    if (result)
            //    {
            //        // return RedirectToAction("Index", "Home", new { area = "ADMIN" });

            //        var topSellingProducts = await _productService.GetTopSellingProductsAsync();
            //        var newProducts = (await _productService.GetNewProductsAsync(0, 10)).products;
            //        var giftProducts = (await _productService.GetGiftCategoryProductsAsync(0, 10)).products;
            //        ViewBag.topSellingProducts = topSellingProducts;
            //        ViewBag.newProducts = newProducts;
            //        ViewBag.giftProducts = giftProducts;
            //        return View();
            //    }
            //    return RedirectToAction("NotFound", "Error");
            //}
            //catch(Exception e)
            //{
            //    return RedirectToAction("NotFound", "Error");
            //}

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



    }
}
