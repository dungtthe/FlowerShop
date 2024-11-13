using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
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

		public async Task<ICollection<Supplier>> GetSuppliersAsync()
		{
			var result = await _supplierRepository.GetAllAsync();
			return result.ToList();
		}
	}
}