using FlowerShop.Common.Helpers;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-don-hang")]
    [ApiController]
    public class SaleInvoiceAPIController : ControllerBase
    {
        private readonly ISaleInvoiceService _saleInvoiceService;

        public SaleInvoiceAPIController(ISaleInvoiceService saleInvoiceService)
        {
            _saleInvoiceService = saleInvoiceService;
        }

        [HttpPost("cho-xac-nhan")]
        public async Task<IActionResult> ChoXacNhan([FromBody] RequestDeleteByIdViewModel reqData)
        {
            if (reqData == null)
            {
                return Ok(new { success = false, message = "Không tìm thấy đơn hàng" });
            }
            int? id = reqData.Id;
            var product = await _saleInvoiceService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Ok(new { success = false, message = "Không tìm thấy đơn hàng" });
            }
            await _saleInvoiceService.ChoXacNhan(id ?? -1);

            return Ok(new { success = true, message = "Đơn hàng đã xác nhận thành công!" });
        }
    }
}