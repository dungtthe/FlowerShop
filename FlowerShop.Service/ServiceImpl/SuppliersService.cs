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
	public class SuppliersService : ISuppliersService
	{
		private readonly ISupplierRepository _supplierRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SuppliersService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
		{
			_supplierRepository = supplierRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Supplier> GetSingleById(int id)
		{
			if (id == -1)
			{
				return null;
			}
			var supplier = await _supplierRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
			return supplier;
		}

		public async Task<ICollection<Supplier>> GetSuppliersAsync()
		{
			var result = await _supplierRepository.GetAllAsync();
			return result.ToList();
		}

		public async Task<Supplier> UpdateAsync(Supplier supplier)
		{
			var result = _supplierRepository.Update(supplier);
			if (result != null)
			{
				await _unitOfWork.Commit();
			}
			return result;
		}
	}
}