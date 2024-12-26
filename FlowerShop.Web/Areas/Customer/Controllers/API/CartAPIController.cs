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

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductInCartAsync(int id, int quantity)
        {
            // Lấy thông tin người dùng từ context
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);

            // Gọi service để cập nhật số lượng sản phẩm
            var rsp = await _cartService.UpdateProductInCartAsync(appUser, id, quantity);

            // Kiểm tra kết quả trả về từ service
            if (rsp.Id == ResponeMessage.NOT_FOUND)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = rsp.Message });
            }

            if (rsp.Id == ResponeMessage.ERROR)
            {
                return BadRequest(new { message = rsp.Message });
            }

            // Trả về kết quả thành công
            return Ok(new { message = rsp.Message });
        }
        [HttpGet("check")]
        public async Task<IActionResult> CheckProductInCartAsync(int id)
        {
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);

            // Kiểm tra sản phẩm có trong giỏ hàng của người dùng không
            var cart = await _cartService.GetCartByUserIdAsync(appUser.Id);
            if (cart == null)
            {
                return Ok(new { exists = false });
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == id);
            if (cartDetail != null)
            {
                return Ok(new { exists = true, existingQuantity = cartDetail.Quantity });
            }

            return Ok(new { exists = false });
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountCartItemsAsync()
        {
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);

            // Lấy giỏ hàng của người dùng từ dịch vụ cartService
            var cart = await _cartService.GetCartByUserIdAsync(appUser.Id);

            if (cart == null)
            {
                // Nếu không có giỏ hàng, trả về số lượng sản phẩm bằng 0
                return Ok(new { count = 0 });
            }

            // Tính tổng số lượng sản phẩm trong giỏ hàng
            int totalItems = cart.CartDetails.Sum(cd => cd.Quantity);

            // Trả về kết quả dưới dạng JSON với trường count
            return Ok(new { count = totalItems });
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
