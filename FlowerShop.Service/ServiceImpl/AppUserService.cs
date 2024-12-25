using FlowerShop.Common.Helpers;
using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlowerShop.Service.ServiceImpl
{
	public class AppUserService : IAppUserService
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IAppUserRepository _appUserRepository;

		public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAppUserRepository appUserRepository)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_appUserRepository = appUserRepository;
		}

		public async Task<AppUser> GetAppUserByContextAsync(HttpContext context)
		{
			return await _userManager.GetUserAsync(context.User);
		}

		public async Task<AppUser> GetUserByEmailAsync(string email)
		{
			var appUser = await _userManager.FindByEmailAsync(email);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					appUser = null;
				}
			}

			return appUser;
		}

		public async Task<AppUser> GetUserByIdAsync(string id)
		{
			var appUser = await _userManager.FindByIdAsync(id);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					appUser = null;
				}
			}

			return appUser;
		}

		public async Task<AppUser> GetUserByUserNameAsync(string userName)
		{
			var appUser = await _userManager.FindByNameAsync(userName);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					appUser = null;
				}
			}

			return appUser;
		}

		public async Task<bool?> IsAdminAsync(string id)
		{
			var appUser = await GetUserByIdAsync(id);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					return null;
				}

				if (appUser.RolesName.Contains(ConstRole.ADMIN))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return null;
		}

		public async Task<bool?> IsCustomerAsync(string id)
		{
			var appUser = await GetUserByIdAsync(id);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					return null;
				}

				if (appUser.RolesName.Contains(ConstRole.CUSTOMER))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return null;
		}

		public async Task<bool?> IsStaffAsync(string id)
		{
			var appUser = await GetUserByIdAsync(id);
			if (appUser != null)
			{
				appUser.RolesName = await _userManager.GetRolesAsync(appUser);
				if (appUser.RolesName == null || !appUser.RolesName.Any())
				{
					return null;
				}

				if (appUser.RolesName.Contains(ConstRole.STAFF))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return null;
		}

		public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
		{
			var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
			return result.Succeeded;
		}

		public async Task<PopupViewModel> UpdateUserAsync(string fullName, DateTime birtDay, string email, string phone, string bytesImage, HttpContext context)
		{
			var user = await GetAppUserByContextAsync(context);

			if (user == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Dữ liệu người dùng không hợp lệ.");
			}

			user.FullName = fullName;
			user.Email = email;
			user.BirthDay = birtDay;
			user.PhoneNumber = phone;

			if (string.IsNullOrEmpty(fullName) || fullName.Length > 300)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Họ tên không hợp lệ.");
			}

			if (!Validator.IsValidEmail(email))
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Email không hợp lệ.");
			}
			if (!Validator.IsValidPhoneNumber(phone))
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Số điện thoại không hợp lệ.");
			}

			//check unique email,phone
			var rsFindUser = await _userManager.FindByEmailAsync(email);
			if (rsFindUser != null && rsFindUser.Id != user.Id)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Email đã tồn tại.");
			}

			rsFindUser = (await _appUserRepository.FindAsync(u => u.PhoneNumber == phone && u.Id != user.Id)).FirstOrDefault();
			if (rsFindUser != null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Số điện thoại đã tồn tại.");
			}

			//xử lý image
			if (!string.IsNullOrEmpty(bytesImage))
			{
				byte[] imageBytes = Converter.ConvertStringToByteArray(bytesImage);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					Image image = Converter.ByteToImage(imageBytes);
					if (image == null)
					{
						return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Có lỗi xảy ra khi lưu ảnh.");
					}

					// fileName random
					var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
						 + Path.GetExtension(".png");
					var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "users");
					var filePath = Path.Combine(wwwRootPath, fileName);

					image.Save(filePath);
					var imgs = Utils.AddPhotoForProduct(fileName, user.Images);
					user.Images = imgs;
				}
			}

			// Cập nhật dữ liệu vào cơ sở dữ liệu
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Cập nhật thông tin thành công.");
			}

			// Xử lý lỗi nếu không thành công
			var errors = string.Join(", ", result.Errors.Select(e => e.Description));
			return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", $"Không thể cập nhật thông tin: {errors}");
		}

		public async Task<PopupViewModel> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
		{
			if (user == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy người dùng.");
			}

			try
			{
				var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

				if (result.Succeeded)
				{
					return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đổi mật khẩu thành công.");
				}
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", $"Mật khẩu cũ sai");
			}
			catch (Exception ex)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Đã xảy ra lỗi khi đổi mật khẩu. Vui lòng thử lại sau.");
			}
		}
	}
}