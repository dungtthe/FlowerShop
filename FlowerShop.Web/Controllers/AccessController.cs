using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAppUserService _appUserService;
        public AccessController(IAppUserService appUserService)
        {
            this._appUserService = appUserService;
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            //return View();

            //bool result = await _appUserService.LoginAsync("1", "1", false);
            //if (result)
            //{
            //    return RedirectToAction("Index", "Home", new { area = "Admin" });
            //}
            return Content("Có lỗi xảy ra");
        }




        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginViewModel appUserViewModel)
        {

            bool result=await _appUserService.LoginAsync(appUserViewModel.UserName,appUserViewModel.Password,false);
            if(result)
            {
                return Content("đăng nhập thành công");
            }
            

            return RedirectToAction("Login", "Access");
        }
    }
}
