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
    [Route("api/admin/quan-li-nha-cung-cap")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        private readonly ISuppliersService _supplierService;

        public SupplierAPIController(ISuppliersService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] RequestDeleteByIdViewModel reqData)
        {
            if (reqData == null)
            {
                return Ok(new { success = false, message = "Không tìm thấy nhà cung cấp" });
            }
            int? id = reqData.Id;
            var product = await _supplierService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Ok(new { success = false, message = "Không tìm thấy nhà cung cấp" });
            }
            await _supplierService.Delete(id ?? -1);

            return Ok(new { success = true, message = "Nhà cung cấp đã tạm ngưng" });
        }
    }
}