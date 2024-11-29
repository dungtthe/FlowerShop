using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface ISupplierInvoiceService
	{
		Task<SupplierInvoice> GetSingleById(int id);

		Task<ICollection<SupplierInvoice>> GetSuppliersInvoiceAsync();

		Task<ICollection<SupplierInvoice>> GetCancelledSupplierInvoice();

		Task<ICollection<SupplierInvoice>> GetSuccessSupplierInvoice();

		Task<ICollection<SupplierInvoiceDetail>> ChiTietHoaDonNhap(int id);

		Task<float> GetTongTien(int id);
	}
}