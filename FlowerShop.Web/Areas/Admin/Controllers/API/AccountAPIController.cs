using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{
	[Area("ADMIN")]
	[Route("api/admin/quan-li-tai-khoan")]
	[ApiController]
	public class AccountAPIController : ControllerBase
	{
		private readonly IAppUserService _appUserService;

		public AccountAPIController(IAppUserService appUserService)
		{
			_appUserService = appUserService;
		}

		[HttpPost("cap-nhat-thong-tin")]
		public async Task<IActionResult> UpdateUser([FromBody] AppUser updatedUser)
		{
			if (updatedUser == null)
			{
				return BadRequest(new { message = "Dữ liệu người dùng không hợp lệ." });
			}

			var result = await _appUserService.UpdateUserAsync(updatedUser, HttpContext);
			if (result.Type == PopupViewModel.SUCCESS)
			{
				return Ok(new { message = result.Message });
			}

			return BadRequest(new { message = result.Message });
		}
	}
}