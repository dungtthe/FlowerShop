using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
	public class CustomerService : ICustomerService
	{
		private readonly IAppUserRepository _appUserRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly FlowerShopContext _context;

		public CustomerService(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, FlowerShopContext context)
		{
			_appUserRepository = appUserRepository;
			_unitOfWork = unitOfWork;
			_context = context;
		}

		public async Task<PopupViewModel> Delete(int id)
		{
			var customer = await _appUserRepository.GetSingleByIdAsync(id);
			if (customer == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			customer.IsDelete = true;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã xóa thành công");
		}

		public async Task<ICollection<AppUser>> GetCustomerAsync()
		{
			var khachhang = _context.UserRoles.Where(x => x.RoleId == "2").ToList();

			var result = (await _appUserRepository.GetAllAsync()).ToList();
			var customerList = new List<AppUser>();
			foreach (var appUser in result)
			{
				foreach (var appRole in khachhang)
				{
					if (appRole.UserId == appUser.Id)
						customerList.Add(appUser);
				}
			}
			return customerList;
		}

		public async Task<AppUser> GetSingleById(int id)
		{
			if (id == -1)
			{
				return null;
			}
			var customer = await _appUserRepository.SingleOrDefaultWithIncludeAsync(x => x.Id == id.ToString());
			return customer;
		}

		public async Task<AppUser> UpdateAsync(AppUser customer)
		{
			var result = _appUserRepository.Update(customer);
			if (result != null)
			{
				await _unitOfWork.Commit();
			}
			return result;
		}
	}
}