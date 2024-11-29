using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
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
        public HomeController(IAppUserService appUserService,ICategoryService categoryService,IProductService productService)
        {
            _appUserService = appUserService;
            _categoryService = categoryService;
            _productService = productService;
        }


        [Route("")]
        [Route("home")]
        public async Task<IActionResult> IndexAsync()
        {
            bool result = await _appUserService.LoginAsync("1", "1", false);
            if (result)
            {
                // return RedirectToAction("Index", "Home", new { area = "ADMIN" });

                var products = await _productService.GetProductsForIndexInCustomerAsync();
                ViewBag.products= products;
                return View();
            }
            return Content("Có lỗi xảy ra");

        }
    }
}
