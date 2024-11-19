using FlowerShop.Common.Helpers;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{

    [Area("ADMIN")]
    [Route("api/admin/quan-li-nhan-vien")]
    [ApiController]
    public class AppUserAPIController : ControllerBase
    {

        private readonly IUserService _userService;
        
        public AppUserAPIController(IUserService userService)
        {
            _userService = userService;
           
        }

        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage([FromBody] RequestImage request)
        {
            var userId= request.Id.ToString();
            var user = await _userService.FindOneWithIncludeByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", request.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var imgs = Utils.RemovePhotoForProduct(request.FileName, user.Images);
            user.Images = imgs;
            await _userService.UpdateAsync(user);

            return Ok(new { message = "Ảnh đã được xóa thành công." });
        }





        [HttpPut("set-default-img")]
        public async Task<IActionResult> SetDefaultImg([FromBody] RequestImage request)
        {
            var userId = request.Id.ToString();
            var user = await _userService.FindOneWithIncludeByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var imgs = Utils.SetDefaultProductImage(request.FileName, user.Images);
            user.Images = imgs;
            await _userService.UpdateAsync(user);

            return Ok(new { message = "Thành công." });
        }
    }
}
