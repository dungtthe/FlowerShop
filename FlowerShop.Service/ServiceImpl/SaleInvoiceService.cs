using FlowerShop.Common.MyConst;
using FlowerShop.DataAccess;
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
	public class SaleInvoiceService : ISaleInvoiceService
	{
		private readonly ISaleInvoiceRepository _saleInvoiceRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SaleInvoiceService(ISaleInvoiceRepository saleInvoiceRepository, IUnitOfWork unitOfWork)
		{
			_saleInvoiceRepository = saleInvoiceRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<ICollection<SaleInvoice>> GetSaleInvoiceWithIcludeAsync()
		{
			var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
			var result = new List<SaleInvoice>();
			foreach (var item in tempresult)
			{
				if (item.Status == ConstStatusSaleInvoice.DANG_CHO)
					result.Add(item);
			}
			return result.ToList();
		}
	}
}