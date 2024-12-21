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
	[Route("api/admin/quan-li-khach-hang")]
	[ApiController]
	public class CustomerAPIController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerAPIController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete([FromBody] RequestDeleteByIdViewModel reqData)
		{
			if (reqData == null)
			{
				return BadRequest(new { message = "Không tìm thấy khách hàng" });
			}
			string? id = reqData.Id.ToString();
			var customer = await _customerService.GetSingleById(id);
			if (customer == null)
			{
				return BadRequest(new { message = "Không tìm thấy khách hàng" });
			}
			await _customerService.Delete(customer);
			return Ok(new { message = "Đã khóa tài khoản của khách hàng" });
		}

		[HttpGet("chi-tiet-khach-hang/{id}")]
		public async Task<IActionResult> ChiTietKhachHang(int id)
		{
			if (id <= 0)
			{
				return BadRequest(new { message = "Không tìm thấy khách hàng" });
			}

			var khachhang = await _customerService.ChiTietKhachHang(id.ToString());
			if (khachhang == null)
			{
				return NotFound(new { message = "Không tìm khách hàng" });
			}

			return Ok(khachhang);
		}
	}
}