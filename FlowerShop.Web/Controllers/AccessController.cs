using FlowerShop.Common.MyConst;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Controllers
{
	public class AccessController : Controller
	{
		private readonly IAppUserService _appUserService;
		private readonly SignInManager<AppUser> _signInManager;
		public AccessController(IAppUserService appUserService, SignInManager<AppUser> signInManager)
		{
			this._appUserService = appUserService;
			this._signInManager = signInManager;
		}

		[HttpGet("/login")]
		public IActionResult Login(string? ReturnUrl)
		{
			try
			{
				TempData["ReturnUrl"] = ReturnUrl;
				return View();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpPost("/login")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([Bind("UserName,Password")] LoginViewModel appUserViewModel)
		{
			bool result = await _appUserService.LoginAsync(appUserViewModel.UserName, appUserViewModel.Password, false);
			if (result)
			{

				string? returnUrl = TempData["ReturnUrl"]?.ToString();

				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
				{
					return Redirect(returnUrl);
				}


				var appUser = await _appUserService.GetUserByUserNameAsync(appUserViewModel.UserName);
				if (appUser == null)
				{
					return RedirectToAction("Login", "Access");
				}

				var isCus = appUser.RolesName.Contains(ConstRole.CUSTOMER);
				if (isCus)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction("Index", "Home", new { area = "ADMIN" });
				}
			}

			return RedirectToAction("Login", "Access");
		}

		[HttpGet("/forgetpassword")]
		public IActionResult ForgetPassword()
		{
			return View();
		}


		[HttpGet("/register")]
		public IActionResult Register()
		{
			return View();
		}


        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}
