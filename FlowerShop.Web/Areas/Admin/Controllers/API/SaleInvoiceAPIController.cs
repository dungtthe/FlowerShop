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
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var order = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.ChoXacNhan(id ?? -1);

			return Ok(new { message = "Đơn hàng đã xác nhận thành công!" });
		}

		[HttpPost("da-xac-nhan")]
		public async Task<IActionResult> DaXacNhan([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var order = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.DaXacNhan(id ?? -1);

			return Ok(new { message = "Đơn hàng đã qua giai đoạn chuẩn bị!" });
		}

		[HttpPost("dang-chuan-bi")]
		public async Task<IActionResult> DangChuanBi([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var order = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.DangChuanBi(id ?? -1);

			return Ok(new { message = "Đơn hàng đã qua giai đoạn giao hàng!" });
		}

		[HttpPost("giao-thanh-cong")]
		public async Task<IActionResult> GiaoThanhCong([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var order = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.GiaoThanhCong(id ?? -1);

			return Ok(new { message = "Đơn hàng đã giao thành công!" });
		}

		[HttpPost("giao-that-bai")]
		public async Task<IActionResult> GiaoThatBai([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var order = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (order == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.GiaoThatBai(id ?? -1);

			return Ok(new { message = "Đơn hàng giao không thành công!" });
		}

		[HttpPost("huy")]
		public async Task<IActionResult> Huy([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			int? id = reqData.Id;
			var product = await _saleInvoiceService.GetSingleById(id ?? -1);
			if (product == null)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}
			await _saleInvoiceService.Huy(id ?? -1);

			return Ok(new { message = "Đơn hàng đã hủy thành công!" });
		}

		[HttpGet("chi-tiet-don-hang/{id}")]
		public async Task<IActionResult> ChiTietDonHang(int id)
		{
			if (id <= 0)
			{
				return BadRequest(new { message = "Không tìm thấy đơn hàng" });
			}

			var saleInvoiceDetail = await _saleInvoiceService.ChiTietDonHang(id);
			if (saleInvoiceDetail == null || !saleInvoiceDetail.Any())
			{
				return NotFound(new { message = "Không tìm thấy đơn hàng" });
			}

			return Ok(saleInvoiceDetail.ToList());
		}
	}
}