using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
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

		public async Task<SaleInvoice> GetSingleById(int id)
		{
			if (id == -1)
			{
				return null;
			}
			var order = await _saleInvoiceRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
			return order;
		}

		public async Task<PopupViewModel> ChoXacNhan(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.DA_XAC_NHAN;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã chuyển sang đã xác nhận");
		}

		public async Task<PopupViewModel> Huy(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.DA_HUY;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã hủy");
		}

		public async Task<PopupViewModel> DaXacNhan(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.DANG_CHUAN_BI;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã chuyển sang đang chuẩn bị");
		}

		public async Task<ICollection<SaleInvoice>> LayCacDonHangDaXacNhan()
		{
			var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
			var result = new List<SaleInvoice>();
			foreach (var item in tempresult)
			{
				if (item.Status == ConstStatusSaleInvoice.DA_XAC_NHAN)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<ICollection<SaleInvoice>> LayCacDonHangDaHuy()
		{
			var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
			var result = new List<SaleInvoice>();
			foreach (var item in tempresult)
			{
				if (item.Status == ConstStatusSaleInvoice.DA_HUY)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<ICollection<SaleInvoice>> LayCacDonHangDangChuanBi()
		{
			var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
			var result = new List<SaleInvoice>();
			foreach (var item in tempresult)
			{
				if (item.Status == ConstStatusSaleInvoice.DANG_CHUAN_BI)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<PopupViewModel> DangChuanBi(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.DANG_GIAO_HANG;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã sẵn sàng và chuyển sang trạng thái giao hàng.");
		}

		public async Task<ICollection<SaleInvoice>> LayCacDonHangDangGiao()
		{
			var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
			var result = new List<SaleInvoice>();
			foreach (var item in tempresult)
			{
				if (item.Status == ConstStatusSaleInvoice.DANG_GIAO_HANG)
					result.Add(item);
			}
			return result.ToList();
		}

		public async Task<PopupViewModel> GiaoThanhCong(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã giao thành công!");
		}

		public async Task<PopupViewModel> GiaoThatBai(int id)
		{
			var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
			if (order == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			order.Status = ConstStatusSaleInvoice.GIAO_HANG_THAT_BAI;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng giao không thành công!");
		}
	}
}