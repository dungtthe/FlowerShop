using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> UpdateAsync(AppUser appUser);
        Task<AppUser> GetSingleById(string? id);
        //Task DeleteAsync(string? id);
        Task<PopupViewModel> Delete(AppUser appUser);
        Task<AppUser> AddAsync(AppUser appUser);
    }
}
