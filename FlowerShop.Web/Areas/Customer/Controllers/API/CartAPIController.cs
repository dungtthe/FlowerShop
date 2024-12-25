using FlowerShop.Common.Template;
using FlowerShop.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{


    [Area("CUSTOMER")]
    [Route("/api/cart")]
    [ApiController]
    [Authorize]
    public class CartAPIController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IAppUserService _appUserService;
        public CartAPIController(ICartService cartService, IAppUserService appUserService)
        {
            _cartService = cartService;
            _appUserService = appUserService;
        }

        [HttpGet("add")]
        public async Task<IActionResult> AddProductToCartAsync(int id, int quantity)
        {
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            var rsp = await _cartService.AddProductToCartAsync(appUser, id, quantity);

            if (rsp.Id == ResponeMessage.NOT_FOUND)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (rsp.Id == ResponeMessage.ERROR)
            {
                return BadRequest(new { message = rsp.Message });
            }

            return Ok(new { message = rsp.Message });
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteProductFromCartAsync(int id)
        {
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            var rsp = await _cartService.DeleteProductFromCartAsync(appUser, id);

            if (rsp.Id != ResponeMessage.NOT_FOUND)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (rsp.Id == ResponeMessage.ERROR)
            {
                return BadRequest(new { message = rsp.Message });
            }

            return Ok();
        }
    }
}
