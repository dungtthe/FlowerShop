using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

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
			catch(Exception ex)
			{
                return BadRequest(new { message = "Có lỗi xảy ra" });
            }
        }
	}

	
}