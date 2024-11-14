using FlowerShop.Common.Helpers;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{

    [Area("ADMIN")]
    [Route("api/admin/quan-li-kho")]
    [ApiController]
    public class ProductItemAPIController:ControllerBase
    {

        private readonly IProductItemService _productItemService;
        private readonly IProductProductItemService _productProductItemService;
        public ProductItemAPIController(IProductItemService productItemService, IProductProductItemService productProductItemService)
        {
            _productItemService = productItemService;
            _productProductItemService = productProductItemService;
        }

        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage([FromBody] RequestImage request)
        {
            var product = await _productItemService.GetSingleById(request.Id);
            if (product == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", request.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var imgs = Utils.RemovePhotoForProduct(request.FileName, product.Images);
            product.Images = imgs;
            await _productItemService.UpdateAsync(product);

            return Ok(new { message = "Ảnh đã được xóa thành công." });
        }





        [HttpPut("set-default-img")]
        public async Task<IActionResult> SetDefaultImg([FromBody] RequestImage request)
        {
            var product = await _productItemService.GetSingleById(request.Id);
            if (product == null)
            {
                return NotFound();
            }
            var imgs = Utils.SetDefaultProductImage(request.FileName, product.Images);
            product.Images = imgs;
            await _productItemService.UpdateAsync(product);

            return Ok(new { message = "Thành công." });
        }
    }
}
