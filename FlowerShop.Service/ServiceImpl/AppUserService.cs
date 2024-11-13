using FlowerShop.Common.MyConst;
using FlowerShop.DataAccess.Models;
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

        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
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
            var roles= await GetRolesName(context);
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

    }
}
