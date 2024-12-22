using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers.API
{
	[ApiController]
	[Route("api/admin/quan-li-tham-so")]
	public class ParameterAPIController : ControllerBase
	{
		private readonly IParameterService _parameterService;

		public ParameterAPIController(IParameterService parameterService)
		{
			_parameterService = parameterService;
		}

		[HttpPost("luu")]
		public async Task<IActionResult> SaveParameter([FromBody] ParameterConfiguration request)
		{
			if (request == null)
			{
				return BadRequest(new { message = "Không tìm thấy tham số" });
			}
			await _parameterService.Update(request);
			return Ok(new { message = "Cập nhật tham số thành công!" });
		}
	}
}