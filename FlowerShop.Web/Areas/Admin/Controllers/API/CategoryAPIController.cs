using FlowerShop.Common.Template;
using FlowerShop.Common.ViewModels;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{


    [Area("ADMIN")]
    [Route("api/admin/quan-li-dm")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryAPIController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] RequestDeleteByIdViewModel reqData)
        {
            if(reqData == null)
            {
                return BadRequest(new { message = "Có lỗi xảy ra" });
            }
            int id = reqData.Id??-1;
            var category = await _categoryService.FindOneWithIncludeByIdAsync(id);
            if (category == null)
            {
                return BadRequest(new { message = "Không tìm thấy danh mục để xóa" });
            }

            ResponeMessage rsp = await _categoryService.DeleteAsync(id);

            if (rsp.Id == ResponeMessage.ERROR)
            {
                return BadRequest(new { message = rsp.Message });
            }
            else
            {
                return Ok(new { message = rsp.Message });
            }
        }
    }
}
