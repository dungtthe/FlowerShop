using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class UserService : IUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly FlowerShopContext _context;
        private readonly IAppUserService _appUserService;

        public UserService(IAppUserRepository appUserRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IAppUserService appUserService, FlowerShopContext context)
        {
            _unitOfWork = unitOfWork;
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _context = context;
            _appUserService = appUserService;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //var nhanvien = _context.UserRoles.Where(x => x.Rol == "3").ToList();
            //var result = (await _appUserRepository.GetAllAsync()).ToList();
            //var appUserList = new List<AppUser>();
            //foreach (var appUser in result)
            //{
            //    foreach (var appRole in nhanvien)
            //    {
            //        if (appRole.UserId == appUser.Id)
            //            appUserList.Add(appUser);
            //    }
            //}

            var users = await _appUserRepository.GetAllAsync();
            if (users != null)
            {
                foreach (var user in users)
                {
                    user.RolesName = await _userManager.GetRolesAsync(user);
                }
            }

            return users.Where(u => u.RolesName.Contains(ConstRole.STAFF));
        }

        public async Task<AppUser> GetSingleById(string? id)
        {
            if (id == "-1")
            {
                return null;
            }
            var appUser = await _appUserRepository.FindByIdAsync(id);
            return appUser;
        }

        public async Task<AppUser> UpdateAsync(AppUser appUser)
        {
            var result = _appUserRepository.Update(appUser);
            if (result != null)
            {
                await _unitOfWork.Commit();
            }
            return result;
        }

        public async Task<PopupViewModel> Delete(AppUser appUser)
        {
            try
            {
                var staff = await _appUserService.GetUserByUserNameAsync(appUser.UserName);
                if (staff == null)
                {
                    return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Không tìm thấy nhân viên!");
                }
                staff.IsLock = true;
                var rs = await UpdateAsync(appUser);
                if (rs == null)
                {
                    return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
                }
                return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã khóa tài khoản nhân viên thành công");
            }
            catch
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
            }
        }

        public async Task<AppUser> AddAsync(AppUser appUser)
        {
            var rs = await _appUserRepository.AddAsync(appUser);
            if (rs != null)
            {
                await _unitOfWork?.Commit();
            }
            return rs;
        }

        public async Task<AppUser> FindOneWithIncludeByIdAsync(string id)
        {
            if (id == "-1")
            {
                return null;
            }
            var user = await _appUserRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
            return user;
        }
    }
}