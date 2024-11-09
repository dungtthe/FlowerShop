using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers
{


    [Area("Customer")]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IAppUserService _appUserService;
        public HomeController(IAppUserService appUserService)
        {
            this._appUserService = appUserService;
        }


        [Route("")]
        [Route("home")]
        public async Task<IActionResult> IndexAsync()
        {
            bool result = await _appUserService.LoginAsync("1", "1", false);
            if (result)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return Content("Có lỗi xảy ra");

        }
    }
}
