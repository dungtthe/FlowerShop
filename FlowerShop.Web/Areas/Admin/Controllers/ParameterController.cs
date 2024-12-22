using FlowerShop.DataAccess;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-tham-so")]
	public class ParameterController : Controller
	{
		private readonly FlowerShopContext _context;

		private readonly IParameterService _parameterService;

		public ParameterController(FlowerShopContext context, IParameterService parameterService)
		{
			_context = context;
			_parameterService = parameterService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var data = await _parameterService.GetData();
			return View(data);
		}
	}
}