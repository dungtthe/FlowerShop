﻿using AutoMapper;
using FlowerShop.DataAccess;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-tai-khoan")]
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
			var userInformation = await _appUserService.GetAppUser(HttpContext);
			if (userInformation == null)
			{
				return NotFound();
			}
			HttpContext.Items["UserFullName"] = userInformation.FullName;
			return View(userInformation);
		}
	}
}