using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
    [Area("CUSTOMER")]
    [Route("error")]
    public class ErrorController : Controller
    {
        [HttpGet("notfound")]
        public IActionResult NotFoundAsync()
        {
            return View("NotFound");
        }
    }
}
