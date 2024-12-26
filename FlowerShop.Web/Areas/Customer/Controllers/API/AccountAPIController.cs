using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{
	[Area("CUSTOMER")]
	[Route("/api/cart")]
	[ApiController]
	[Authorize]
	public class AccountAPIController : ControllerBase
	{
		private readonly IAppUserService _appUserService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IWebHostEnvironment _env;

		public AccountAPIController(IAppUserService appUserService, UserManager<AppUser> userManager, IWebHostEnvironment env)
		{
			_appUserService = appUserService;
			_userManager = userManager;
			_env = env;
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateUser(object? data)
		{
			try
			{
				if (data == null)
				{
					return BadRequest(new { message = "Dữ liệu người dùng không hợp lệ." });
				}

				JObject objData = (JObject)JsonConvert.DeserializeObject(data.ToString());

				string fullName = (string)objData["FullName"];
				DateTime birthDay = (DateTime)objData["BirthDay"];
				string email = (string)objData["Email"];
				string phone = (string)objData["PhoneNumber"];
				string byteImages = (string)objData["BytesImage"];

				var result = await _appUserService.UpdateUserAsync(fullName, birthDay, email, phone, byteImages, HttpContext);
				if (result.Type == PopupViewModel.SUCCESS)
				{
					return Ok(new { message = result.Message });
				}

				return BadRequest(new { message = result.Message });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = "Có lỗi xảy ra" });
			}
		}
	}
}