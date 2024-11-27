using FlowerShop.Common.Helpers;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{

    [Area("ADMIN")]
    [Route("api/admin/quan-li-kho")]
    [ApiController]
    public class ProductItemAPIController : ControllerBase
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
            var product = await _productItemService.FindOneWithIncludeByIdAsync(request.Id);
            if (product == null)
            {
                return BadRequest(new { message = "Không tìm thấy sản phẩm chứa ảnh tương ứng" });
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
            var product = await _productItemService.FindOneWithIncludeByIdAsync(request.Id);
            if (product == null)
            {
                return BadRequest(new {message="Không tìm thấy sản phẩm chứa ảnh tương ứng"});
            }
            var imgs = Utils.SetDefaultProductImage(request.FileName, product.Images);
            product.Images = imgs;
            await _productItemService.UpdateAsync(product);

            return Ok(new { message = "Thành công." });
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] RequestDeleteByIdViewModel reqData)
        {

            if (reqData == null)
            {
                return BadRequest(new { message = "Không tìm thấy sản phẩm để xóa" });
            }

            int? id = reqData.Id;


            var product = await _productItemService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (product == null)
            {
                return BadRequest(new { message = "Không tìm thấy sản phẩm để xóa" });
            }

            var checkExist = await _productProductItemService.CheckExistPrductItem(product.Id);
            if (checkExist)
            {
                return BadRequest(new { message = "Sản phẩm này đang được bán nên không thể xóa" });
            }
            await _productItemService.DeleteAsync(id ?? -1);

            return Ok(new { message = "Xóa sản phẩm thành công" });
        }
    }
}
