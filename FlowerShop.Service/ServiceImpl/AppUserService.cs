using FlowerShop.Common.MyConst;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
				appUser.RolesName= await _userManager.GetRolesAsync(appUser);
				if(appUser.RolesName==null || !appUser.RolesName.Any())
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
	}
}
