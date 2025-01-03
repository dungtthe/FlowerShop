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
	public interface ISaleInvoiceService
	{

		Task<SaleInvoice> GetSaleInvoiceByProductIdAsync(int productId);

        Task<SaleInvoice> GetSaleInvoiceByIdAsync(int saleInvoiceId);

        Task<ICollection<SaleInvoice>> GetSaleInvoiceWithIcludeAsync();

		Task<ICollection<SaleInvoice>> LayCacDonHangDaXacNhan();

		Task<ICollection<SaleInvoice>> LayCacDonHangDaHuy();

		Task<ICollection<SaleInvoice>> LayCacDonHangDangChuanBi();

		Task<ICollection<SaleInvoice>> LayCacDonHangDangGiao();

		Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThanhCong();

		Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThatBai();

		Task<ICollection<SaleInvoiceDetail>> ChiTietDonHang(int id);

		Task<SaleInvoice> GetSingleById(int id);

		Task<PopupViewModel> ChoXacNhan(int id);

		Task<PopupViewModel> DaXacNhan(int id);

		Task<PopupViewModel> DangChuanBi(int id);

		Task<PopupViewModel> GiaoThanhCong(int id);

		Task<PopupViewModel> GiaoThatBai(int id, string reason);

		Task<PopupViewModel> Huy(int id, string reason);

		Task XuLyDonHangSauKhiHuy(int id);

		Task<double> TongTienCuaMotDonHang(int id);

		Task<double> TongDoanhThuThangNay();

		Task<double> TongDoanhThuHomNay();

		Task<int> SoDonHangCho();

		Task<Dictionary<string, object>> GetNineSalesDataByDateRangeAsync(DateTime? startDate, DateTime? endDate);

		Task<Dictionary<string, object>> GetSalesDataByDateRangeAsync(DateTime? startDate, DateTime? endDate);

		Task<Dictionary<string, object>> GetTopSellingProductsAsync(DateTime? startDate, DateTime? endDate);

		Task<Dictionary<string, object>> GetAllSale(DateTime? startDate, DateTime? endDate);

		Task<ResponeMessage> ConfirmCheckoutAsync(AppUser user, string fullName, string phoneNumber, string address, string note, int shippingFee, int selectedPaymentMethodId, string sProductIds);
        Task<ICollection<SaleInvoice>> LayCacDonHangChoXacNhanCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangDaXacNhanCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangDangChuanBiCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangDangGiaoCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThanhCongCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThatBaiCuaNguoiDung(string userId);

        Task<ICollection<SaleInvoice>> LayCacDonHangDaHuyCuaNguoiDung(string userId);

    }
}