using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
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
			var supplierInvoiceDetailList = await _supplierInvoiceDetailRepository.GetAllWithIncludeAsync(s => s.ProductItem);
			var supplierInvoiceDetail = supplierInvoiceDetailList.Where(s => s.SupplierInvoiceId == id);
			if (supplierInvoiceDetail == null)
			{
				return new List<SupplierInvoiceDetail>();
			}
			return supplierInvoiceDetail.ToList();
		}

		public async Task<ICollection<SupplierInvoice>> GetCancelledSupplierInvoice()
		{
			var supplierInvoiceListTemp = await _supplierInvoiceRepository.GetAllWithIncludeAsync(s => s.Supplier, c => c.SupplierInvoiceDetails);
			var result = new List<SupplierInvoice>();
			foreach (var item in supplierInvoiceListTemp)
			{
				if (item.Status == ConstStatusSupplierInvoice.DA_HUY)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<ICollection<SupplierInvoice>> GetSuccessSupplierInvoice()
		{
			var supplierInvoiceListTemp = await _supplierInvoiceRepository.GetAllWithIncludeAsync(s => s.Supplier, c => c.SupplierInvoiceDetails);
			var result = new List<SupplierInvoice>();
			foreach (var item in supplierInvoiceListTemp)
			{
				if (item.Status == ConstStatusSupplierInvoice.HOAN_TAT)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<PopupViewModel> XacNhanDonNhap(int id)
		{
			var invoice = await _supplierInvoiceRepository.GetSingleByIdAsync(id);
			if (invoice == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			invoice.Status = ConstStatusSupplierInvoice.HOAN_TAT;
			await XuLyDonNhapSauKhiXacNhan(id);
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn nhập hàng đã được xác nhận");
		}

		public async Task<PopupViewModel> HuyDonNhap(int id, string reason)
		{
			var invoice = await _supplierInvoiceRepository.GetSingleByIdAsync(id);
			if (invoice == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			invoice.Status = ConstStatusSupplierInvoice.DA_HUY;
			invoice.Note = reason;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn nhập hàng đã được hủy");
		}

		public async Task XuLyDonNhapSauKhiXacNhan(int id)
		{
			var chiTietDonNhapList = await ChiTietHoaDonNhap(id);
			foreach (var item in chiTietDonNhapList)
			{
				item.ProductItem.Quantity += item.Quantity;
			}
		}

		public async Task<double> TongTienCuaMotHoaDonNhap(int id)
		{
			double tongtien = 0;
			var chitietdonnhaphang = await ChiTietHoaDonNhap(id);
			foreach (var item in chitietdonnhaphang)
			{
				tongtien += item.Quantity * item.UnitPrice;
			}

			return tongtien;
		}

		public async Task<double> TongChiThangNay()
		{
			double tongtien = 0;
			var donNhapHangGiaoThanhCong = await GetSuccessSupplierInvoice();
			// Lọc các đơn nhập hàng thuộc tháng hiện tại
			var donNhapHangTrongThangNay = donNhapHangGiaoThanhCong
				.Where(d => d.CreateDay.Month == DateTime.Now.Month && d.CreateDay.Year == DateTime.Now.Year);

			// Tính tổng tiền của các đơn hàng trong tháng này
			foreach (var item in donNhapHangTrongThangNay)
			{
				var tien = await TongTienCuaMotHoaDonNhap(item.Id);
				tongtien += tien;
			}
			return tongtien;
		}
	}
}