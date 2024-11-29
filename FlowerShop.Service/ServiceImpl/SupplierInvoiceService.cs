using FlowerShop.Common.MyConst;
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
	public class SupplierInvoiceService : ISupplierInvoiceService
	{
		private readonly ISupplierInvoiceRepository _supplierInvoiceRepository;
		private readonly ISupplierInvoiceDetailRepository _supplierInvoiceDetailRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SupplierInvoiceService(ISupplierInvoiceRepository supplierInvoiceRepository, ISupplierInvoiceDetailRepository supplierInvoiceDetailRepository, IUnitOfWork unitOfWork)
		{
			_supplierInvoiceRepository = supplierInvoiceRepository;
			_supplierInvoiceDetailRepository = supplierInvoiceDetailRepository;
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

		public async Task<ICollection<SupplierInvoice>> GetSuppliersInvoiceAsync()
		{
			var supplierInvoiceListTemp = await _supplierInvoiceRepository.GetAllWithIncludeAsync(s => s.Supplier, c => c.SupplierInvoiceDetails);
			var result = new List<SupplierInvoice>();
			foreach (var item in supplierInvoiceListTemp)
			{
				if (item.Status == ConstStatusSupplierInvoice.CHO_XAC_NHAN)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<float> GetTongTien(int id)
		{
			var chiTietHoaDonNhap = await ChiTietHoaDonNhap(id);
			float tongTien = 0;
			foreach (var item in chiTietHoaDonNhap)
			{
				tongTien += (item.Quantity * item.UnitPrice);
			}

			return tongTien;
		}

		public async Task<ICollection<SupplierInvoiceDetail>> ChiTietHoaDonNhap(int id)
		{
			var supplierInvoiceDetailList = await _supplierInvoiceDetailRepository.GetAllWithIncludeAsync(s => s.ProductItem, p => p.SupplierInvoice);
			var supplierInvoiceDetail = supplierInvoiceDetailList.Where(s => s.SupplierInvoiceId == id).ToList();
			if (supplierInvoiceDetail.Count == 0)
			{
				return new List<SupplierInvoiceDetail>();
			}
			return supplierInvoiceDetail;
		}
	}
}