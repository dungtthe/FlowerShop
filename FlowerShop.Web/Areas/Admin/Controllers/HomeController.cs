using Microsoft.AspNetCore.Mvc;
using FlowerShop.Web.ViewModels;

namespace FlowerShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
