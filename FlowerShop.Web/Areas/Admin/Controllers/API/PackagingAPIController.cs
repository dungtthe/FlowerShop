using FlowerShop.Common.Helpers;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{
	[Area("ADMIN")]
	[Route("api/admin/quan-li-cach-dong-goi")]
	[ApiController]
	public class PackagingAPIController : ControllerBase
	{
		private readonly IPackagingService _packagingService;

		public PackagingAPIController(IPackagingService packagingService)
		{
			_packagingService = packagingService;

		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete([FromBody] RequestDeleteByPackagingIdViewModel? req)
		{
			if (req == null)
			{
				return Ok(new { success = false, message = "Không tìm thấy cách đóng gói để xóa" });
			}
			var packaging = await _packagingService.FindOneById(req.Id);
			if (packaging == null)
			{
				return Ok(new { success = false, message = "Không tìm thấy cách đóng gói để xóa" });
			}
			PopupViewModel rsp = await _packagingService.Delete(packaging);
			return Ok(new { success = (rsp.Type == PopupViewModel.SUCCESS) ? true : false, message = rsp.Message });
		}
	}
}
