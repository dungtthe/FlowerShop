using FlowerShop.Common.Helpers;
using FlowerShop.Common.ViewModels;
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
		private readonly IAppUserService _appUserService;

		public CustomerAPIController(ICustomerService customerService, IAppUserService appUserService)
		{
			_customerService = customerService;
			_appUserService = appUserService;
		}

		[HttpPost("lock/{username}")]
		public async Task<IActionResult> Lock(string username)
		{
			if (username == null)
			{
				return BadRequest(new { message = "Không tìm khách hàng" });
			}
			var user = await _appUserService.GetUserByUserNameAsync(username);
			if (user == null)
			{
				return BadRequest(new { message = "Không tìm thấy khách hàng" });
			}
			PopupViewModel rsp = await _customerService.Delete(user);
			return Ok(new { success = (rsp.Type == PopupViewModel.SUCCESS) ? true : false, message = rsp.Message });
		}

		[HttpGet("chi-tiet-khach-hang/{username}")]
		public async Task<IActionResult> ChiTietKhachHang(string username)
		{
			if (username == "")
			{
				return BadRequest(new { message = "Không tìm thấy khách hàng" });
			}

			var khachhang = await _customerService.ChiTietKhachHang(username);
			if (khachhang == null)
			{
				return NotFound(new { message = "Không tìm thấy khách hàng" });
			}

			return Ok(khachhang);
		}
	}
}