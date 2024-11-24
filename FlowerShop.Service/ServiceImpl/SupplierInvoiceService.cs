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
	public class SupplierInvoiceService : ISupplierInvoice
	{
		private readonly ISupplierInvoiceRepository _supplierInvoiceRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SupplierInvoiceService(ISupplierInvoiceRepository supplierInvoiceRepository, IUnitOfWork unitOfWork)
		{
			_supplierInvoiceRepository = supplierInvoiceRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<SupplierInvoice> GetSingleById(int id)
		{
			if (id == -1)
			{
				return null;
			}
			var supplierInvoice = await _supplierInvoiceRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
			return supplierInvoice;
		}

		public Task<ICollection<SupplierInvoice>> GetSuppliersInvoiceAsync()
		{
			throw new NotImplementedException();
		}
	}
}