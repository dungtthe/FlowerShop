using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser?> FindByIdAsync(string userId);
        Task<AppUser?> FindByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> UpdateUserAsync(AppUser user);
        Task<IdentityResult> DeleteUserAsync(AppUser user);
    }
}
