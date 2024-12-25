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
        Task<AppUser> GetAppUserByContextAsync(HttpContext context);
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<AppUser> GetUserByUserNameAsync(string userName);

		Task<bool?> IsAdminAsync(string id);
		Task<bool?> IsStaffAsync(string id);
		Task<bool?> IsCustomerAsync(string id);
    }
}
