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
    public class PackagingService : IPackagingService
    {
        private readonly IPackagingRepository _packagingRepository;
		private readonly IUnitOfWork _unitOfWork;
		public PackagingService(IPackagingRepository packagingRepository, IUnitOfWork unitOfWork)
        {
            _packagingRepository = packagingRepository;
			_unitOfWork = unitOfWork;
		}

        public async Task<Packaging> FindOneById(int id)
        {
            var rs = (await _packagingRepository.GetSingleByIdAsync(id));
            if(rs==null || rs.IsDelete)
            {
                rs = null;
            }
            return rs;
        }

        public async Task<IEnumerable<Packaging>> GetAllPackagingAsync()
        {
            var ls = (await _packagingRepository.GetAllAsync()).Where(p => p.IsDelete == false);
            return ls;
        }


		public async Task<Packaging> UpdateAsync(Packaging packaging)
		{
			var result = _packagingRepository.Update(packaging);
			if (result != null)
			{
				await _unitOfWork.Commit();
			}
			return result;
		}

		public async Task<PopupViewModel> Delete(Packaging packaging)
		{
			try
			{
				packaging.IsDelete = true;
				var rs = await UpdateAsync(packaging);
				if (rs == null)
				{
					return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
				}
				return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã xóa cách đóng gói thành công");
			}
			catch
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
			}
		}


        public async Task<Packaging> AddAsync(Packaging packaging)
        {
            var rs = await _packagingRepository.AddAsync(packaging);
            if (rs != null)
            {
                await _unitOfWork?.Commit();
            }
            return rs;
        }


    }
}
