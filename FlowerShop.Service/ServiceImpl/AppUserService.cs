using FlowerShop.DataAccess.Models;
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
        public AppUserService(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
            return result.Succeeded;
        }

    }
}
