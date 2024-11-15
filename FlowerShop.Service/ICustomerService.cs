﻿using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface ICustomerService
	{
		Task<ICollection<AppUser>> GetCustomerAsync();

		Task<AppUser> UpdateAsync(AppUser customer);

		Task<AppUser> GetSingleById(int id);

		Task<PopupViewModel> Delete(int id);
	}
}