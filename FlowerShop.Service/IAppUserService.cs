using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface IAppUserService
	{
		Task<bool> LoginAsync(string username, string password, bool isPersistent);

		Task<AppUser> GetAppUser(HttpContext context);

		// Task<ICollection<string>> GetRolesName(HttpContext context);
		Task<bool> IsAdmin(HttpContext context);

		Task<bool> IsCustomer(HttpContext context);

		Task<bool> IsStaff(HttpContext context);

		Task<PopupViewModel> UpdateUserAsync(AppUser updatedUser, HttpContext context);
	}
}