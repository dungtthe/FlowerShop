using FlowerShop.Common.Helpers;
using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{

    [Area("ADMIN")]
    [Route("api/admin/quan-li-sp")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductViewModel productData)
        {
            try
            {

                StringBuilder message = new StringBuilder("");
                if (string.IsNullOrEmpty(productData.Title))
                {
                    message.AppendLine("- Tiêu đề không được để trống!");
                }
                if (string.IsNullOrEmpty(productData.Description))
                {
                    message.AppendLine("- Mô tả không được để trống!");
                }
                if (productData.PriceDefault <= 0)
                {
                    message.AppendLine("- Giá bán phải > 0");
                }
                if (productData.Quantity <= 0)
                {
                    message.AppendLine("- Số lượng phải > 0");
                }
                if (productData.CategoriesId == null || productData.CategoriesId.Count<1)
                {
                    message.AppendLine("- Phải thuộc về ít nhất một danh mục");
                }
                if (productData.ProductItems == null || productData.ProductItems.Count < 1)
                {
                    message.AppendLine("- Phải có ít nhất 1 sản phẩm trong kho");
                }

                if (message.ToString() != "")
                {
                    message = new StringBuilder(message.ToString().Replace("\n", "<br />"));
                    return Ok(new { success = false, message = message.ToString() });
                }
                else
                {
                    List<Tuple<int, int>> productsItem = new List<Tuple<int, int>>();
                    foreach(var item in productData.ProductItems)
                    {
                        productsItem.Add(new Tuple<int, int>(item.Id, item.Quantity));
                    }
                    var rsAddProduct = await _productService.AddNewProductAsync(productData.Title,productData.Description,(decimal)productData.PriceDefault,productData.Quantity,productData.PackagingId,productData.CategoriesId,productsItem);
                    if (rsAddProduct.Id == ResponeMessage.ERROR)
                    {
                        return Ok(new { success = false, message =rsAddProduct.Message });
                    }
                    else
                    {
                        return Ok(new { success = true, message = rsAddProduct.Message });
                    }

                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra" });
            }
        }




        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage([FromBody] RequestImage request)
        {
            var product = await _productService.FindOneByIdAsync(request.Id);
            if (product == null)
            {
                return BadRequest(new {message="Không tìm thấy sản phẩm"});
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", request.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var imgs = Utils.RemovePhotoForProduct(request.FileName, product.Images);
            product.Images = imgs;
            await _productService.UpdateImageAsync(product);

            return Ok(new { message = "Ảnh đã được xóa thành công." });
        }





        [HttpPut("set-default-img")]
        public async Task<IActionResult> SetDefaultImg([FromBody] RequestImage request)
        {
            var product = await _productService.FindOneByIdAsync(request.Id);
            if (product == null)
            {
                return BadRequest(new { message = "Không tìm thấy sản phẩm" });
            }
            var imgs = Utils.SetDefaultProductImage(request.FileName, product.Images);
            product.Images = imgs;
            await _productService.UpdateImageAsync(product);

            return Ok(new { message = "Thành công." });
        }
    }
}
