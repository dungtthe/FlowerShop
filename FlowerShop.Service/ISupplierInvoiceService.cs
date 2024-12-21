using FlowerShop.Common.ViewModels;
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

		Task<PopupViewModel> XacNhanDonNhap(int id);

		Task<PopupViewModel> HuyDonNhap(int id, string reason);

		Task XuLyDonNhapSauKhiXacNhan(int id);

		Task<double> TongTienCuaMotHoaDonNhap(int id);

		Task<double> TongChiThangNay();
	}
}