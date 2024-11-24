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
				return BadRequest(new { message = "Không tìm thấy nhà cung cấp" });
			}
			int? id = reqData.Id;
			var product = await _supplierService.GetSingleById(id ?? -1);
			if (product == null)
			{
				return BadRequest(new { message = "Không tìm thấy nhà cung cấp" });
			}
			await _supplierService.Delete(id ?? -1);

			return Ok(new { message = "Đã ngưng nhận hàng từ nhà cung cấp này" });
		}

		[HttpGet("chi-tiet-nha-cung-cap/{id}")]
		public async Task<IActionResult> ChiTietNhaCungCap(int id)
		{
			if (id <= 0)
			{
				return BadRequest(new { message = "Không tìm thấy nhà cung cấp" });
			}

			var nhaCungCap = await _supplierService.ChiTietNhaCungCap(id);
			if (nhaCungCap == null || !nhaCungCap.Any())
			{
				return NotFound(new { message = "Không tìm nhà cung cấp" });
			}

			return Ok(nhaCungCap.ToList());
		}
	}
}