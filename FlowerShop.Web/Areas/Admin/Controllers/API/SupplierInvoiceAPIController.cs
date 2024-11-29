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
	[Route("api/admin/quan-li-don-nhap")]
	[ApiController]
	public class SupplierInvoiceAPIController : ControllerBase
	{
		private readonly ISupplierInvoiceService _supplierInvoiceService;

		public SupplierInvoiceAPIController(ISupplierInvoiceService supplierInvoiceService)
		{
			_supplierInvoiceService = supplierInvoiceService;
		}

		[HttpPost("xac-nhan")]
		public async Task<IActionResult> XacNhan([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn nhập hàng" });
			}
			int? id = reqData.Id;
			var order = await _supplierInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn nhập hàng" });
			}
			await _supplierInvoiceService.XacNhanDonNhap(id ?? -1);
			return Ok(new { message = "Đơn nhập hàng đã xác nhận thành công!" });
		}

		[HttpPost("huy")]
		public async Task<IActionResult> Huy([FromBody] RequestDeletedOrder reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn nhập hàng" });
			}
			int? id = reqData.Id;
			string? reason = reqData.reason;
			var order = await _supplierInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn nhập hàng" });
			}
			await _supplierInvoiceService.HuyDonNhap(id ?? -1, reason ?? "");
			return Ok(new { message = "Đơn nhập hàng đã hủy thành công!" });
		}
	}
}