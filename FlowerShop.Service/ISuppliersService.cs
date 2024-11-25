﻿using FlowerShop.Common.Template;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface ISuppliersService
	{
		Task<ICollection<Supplier>> GetSuppliersAsync();

		Task<Supplier> UpdateAsync(Supplier supplier);

		Task<Supplier> GetSingleById(int id);

		Task<PopupViewModel> Delete(int id);

		Task<ResponeMessage> AddNewSupplier(string companyName, string taxCode,
				string email, string phone, int type, string images, string description,
				string industry, string address, bool isDelete);

		Task<ICollection<Supplier>> ChiTietNhaCungCap(int id);
	}
}