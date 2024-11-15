using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
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
        public UserService(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appUserRepository = appUserRepository;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var result = (await _appUserRepository.GetAllAsync()).Where(u=>u.IsDelete==false);
            return result;
        }

        public async Task<AppUser> GetSingleById(string? id)
        {
            if (id == "-1")
            {
                return null;
            }
            var appUser = await _appUserRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
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
                appUser.IsLock = true;
                appUser.IsDelete = true;
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
    }
}
