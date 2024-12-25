using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
	public class AppUserService : IAppUserService
	{
		private SignInManager<AppUser> _signInManager;
		private UserManager<AppUser> _userManager;

		public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<AppUser> GetAppUser(HttpContext context)
		{
			return await _userManager.GetUserAsync(context.User);
		}

		public async Task<bool> IsAdmin(HttpContext context)
		{
			var roles = await GetRolesName(context);
			foreach (var role in roles)
			{
				if (role.Equals(ConstRole.ADMIN))
				{
					return true;
				}
			}
			return false;
		}

		public async Task<bool> IsCustomer(HttpContext context)
		{
			var roles = await GetRolesName(context);
			foreach (var role in roles)
			{
				if (role.Equals(ConstRole.CUSTOMER))
				{
					return true;
				}
			}
			return false;
		}

		public async Task<bool> IsStaff(HttpContext context)
		{
			var roles = await GetRolesName(context);
			foreach (var role in roles)
			{
				if (role.Equals(ConstRole.STAFF))
				{
					return true;
				}
			}
			return false;
		}

		public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
		{
			var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
			return result.Succeeded;
		}

		private async Task<ICollection<string>> GetRolesName(HttpContext context)
		{
			return await _userManager.GetRolesAsync(await GetAppUser(context));
		}

		public async Task<PopupViewModel> UpdateUserAsync(AppUser updatedUser, HttpContext context)
		{
			if (updatedUser == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Dữ liệu người dùng không hợp lệ.");
			}

			// Lấy người dùng hiện tại
			var currentUser = await GetAppUser(context);
			if (currentUser == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy người dùng.");
			}

			// Cập nhật thông tin
			currentUser.FullName = updatedUser.FullName;
			currentUser.Email = updatedUser.Email;
			currentUser.PhoneNumber = updatedUser.PhoneNumber;
			currentUser.BirthDay = updatedUser.BirthDay;
			// Cập nhật dữ liệu vào cơ sở dữ liệu
			var result = await _userManager.UpdateAsync(currentUser);
			if (result.Succeeded)
			{
				return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Cập nhật thông tin thành công.");
			}

			// Xử lý lỗi nếu không thành công
			var errors = string.Join(", ", result.Errors.Select(e => e.Description));
			return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", $"Không thể cập nhật thông tin: {errors}");
		}
	}
}