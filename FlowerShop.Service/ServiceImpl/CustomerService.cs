using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
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
		private readonly UserManager<AppUser> _userManager;
		private readonly IAppUserService _appUserService;

		public CustomerService(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, FlowerShopContext context, UserManager<AppUser> userManager, IAppUserService appUserService)
		{
			_appUserRepository = appUserRepository;
			_unitOfWork = unitOfWork;
			_context = context;
			_userManager = userManager;
			_appUserService = appUserService;
		}

		public async Task<AppUser> ChiTietKhachHang(string username)
		{
			var customer = await _appUserService.GetUserByUserNameAsync(username);
			if (customer == null)
			{
				return new AppUser(); // Trả về danh sách rỗng
			}
			return customer;
		}

		public async Task<PopupViewModel> Delete(AppUser appUser)
		{
			try
			{
				var customer = await _appUserService.GetUserByUserNameAsync(appUser.UserName);
				if (customer == null)
				{
					return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Không tìm thấy khách hàng!");
				}
				customer.IsLock = true;
				var rs = await UpdateAsync(appUser);
				if (rs == null)
				{
					return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
				}
				return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã khóa tài khoản khách hàng thành công");
			}
			catch
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Thất bại", "Có lỗi xảy ra");
			}
		}

		public async Task<IEnumerable<AppUser>> GetCustomerAsync()
		{
			var khachhangList = await _appUserRepository.GetAllAsync();
			if (khachhangList != null)
			{
				foreach (var khachhang in khachhangList)
				{
					khachhang.RolesName = await _userManager.GetRolesAsync(khachhang);
				}
			}

			return khachhangList.Where(u => u.RolesName.Contains(ConstRole.CUSTOMER));
		}

		public async Task<AppUser> GetSingleById(string id)
		{
			if (id == "-1")
			{
				return null;
			}
			var customer = await _appUserRepository.SingleOrDefaultWithIncludeAsync(x => x.Id == id.ToString());
			return customer;
		}

		public async Task<AppUser> UpdateAsync(AppUser customer)
		{
			// Kiểm tra xem ảnh có phải là chuỗi hay không. Nếu là chuỗi thì chuyển thành mảng.
			List<string> imageNames = new List<string>();
			string imagesJson = string.Empty;
			// Nếu supplier.Images là một chuỗi, chuyển thành mảng một phần tử
			if (!string.IsNullOrEmpty(customer.Images))
			{
				imageNames.Add(customer.Images); // Thêm tên ảnh vào danh sách
				imagesJson = JsonConvert.SerializeObject(imageNames); // Chuyển thành chuỗi JSON
				customer.Images = imagesJson;
			}
			var result = _appUserRepository.Update(customer);
			if (result != null)
			{
				await _unitOfWork.Commit();
			}
			return result;
		}
	}
}