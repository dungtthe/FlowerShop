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
        public HomeController(IAppUserService appUserService,ICategoryService categoryService)
        {
            _appUserService = appUserService;
            _categoryService = categoryService;
        }


        [Route("")]
        [Route("home")]
        public async Task<IActionResult> IndexAsync()
        {
            bool result = await _appUserService.LoginAsync("1", "1", false);
            if (result)
            {
                // return RedirectToAction("Index", "Home", new { area = "ADMIN" });


                var categories = await _categoryService.GetAllCategoriesWithHierarchy();
                ViewBag.Categories = categories;
                return View();
            }
            return Content("Có lỗi xảy ra");

        }
    }
}
