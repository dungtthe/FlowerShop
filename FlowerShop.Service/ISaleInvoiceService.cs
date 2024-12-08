﻿using FlowerShop.Common.ViewModels;
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

		Task<PopupViewModel> GiaoThatBai(int id);

		Task<PopupViewModel> Huy(int id);

		Task XuLyDonHangSauKhiHuy(int id);
	}
}