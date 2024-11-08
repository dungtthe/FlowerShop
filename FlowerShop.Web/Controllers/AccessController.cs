using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Controllers
{
    public class AccessController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
