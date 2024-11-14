using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
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
	}
}