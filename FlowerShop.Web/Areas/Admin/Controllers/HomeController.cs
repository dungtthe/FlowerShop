﻿using Microsoft.AspNetCore.Mvc;
using FlowerShop.Web.ViewModels;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using Microsoft.AspNetCore.Authorization;

namespace FlowerShop.Web.Areas.Admin.Controllers
{

    [Area("ADMIN")]
    [Route("admin")]
    [Authorize]
    public class HomeController : Controller
    {
        IAppUserService _appUserService;
        public HomeController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [Route("")]
        [Route("home")]
        public async Task< IActionResult> Index()
        {
            AppUser appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);

            return RedirectToAction("Index", "Statistical");
        }
    }
}
