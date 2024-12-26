using FlowerShop.Common.Template;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
	[Area("CUSTOMER")]
	[Route("quan-ly-tai-khoan")]
	public class AccountController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly IAppUserService _appUserService;

		public AccountController(FlowerShopContext context, IAppUserService appUserService)
		{
			_context = context;
			_appUserService = appUserService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var userInformation = await _appUserService.GetAppUserByContextAsync(HttpContext);
			if (userInformation == null)
			{
				return NotFound();
			}
			return View(userInformation);
		}
	}
}