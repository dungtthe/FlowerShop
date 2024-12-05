using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{



    [Area("CUSTOMER")]
    [Route("/api/product")]
    [ApiController]
    public class ProductAPIController:ControllerBase
    {
        [HttpGet("detail")]
        public async Task<IActionResult> DetailProductAsync(int? id)
        {
            return Ok(new { redirect = $"/product/detail?id={id??-1}" });
        }
    }
}
