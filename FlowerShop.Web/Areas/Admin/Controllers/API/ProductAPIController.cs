using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
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

                StringBuilder message = new StringBuilder();
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
                message = new StringBuilder(message.ToString().Replace("\n", "<br />"));


                //// Gọi service để lưu sản phẩm
                //var result = await _productService.AddNewProductAsync();

                //if (result==null)
                //{
                //    return Ok(new { success = true, message = "Thêm sản phẩm thành công." });
                //}
                //else
                //{
                //    return BadRequest(new { success = false, message = "Có lỗi xảy ra khi lưu sản phẩm." });
                //}
                return Ok(new { success = false, message = message.ToString() });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra" });
            }
        }

    }
}
