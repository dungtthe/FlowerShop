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

namespace FlowerShop.Service.ServiceImpl
{
	public class AppUserService : IAppUserService
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public AppUserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
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

		public async Task<PopupViewModel> UpdateUserAsync(AppUser updatedUser, HttpContext context)
		{
			if (updatedUser == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Dữ liệu người dùng không hợp lệ.");
			}

			// Lấy người dùng hiện tại
			var currentUser = await GetAppUserByContextAsync(context);
			if (currentUser == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy người dùng.");
			}

			// Cập nhật thông tin
			currentUser.FullName = updatedUser.FullName;
			currentUser.Email = updatedUser.Email;
			currentUser.PhoneNumber = updatedUser.PhoneNumber;
			currentUser.BirthDay = updatedUser.BirthDay;

			// Kiểm tra xem ảnh có phải là chuỗi hay không. Nếu là chuỗi thì chuyển thành mảng.
			List<string> imageNames = new List<string>();
			string imagesJson = string.Empty;
			// Nếu supplier.Images là một chuỗi, chuyển thành mảng một phần tử
			if (!string.IsNullOrEmpty(updatedUser.Images))
			{
				imageNames.Add(updatedUser.Images); // Thêm tên ảnh vào danh sách
				imagesJson = JsonConvert.SerializeObject(imageNames); // Chuyển thành chuỗi JSON
				currentUser.Images = imagesJson;
			}
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