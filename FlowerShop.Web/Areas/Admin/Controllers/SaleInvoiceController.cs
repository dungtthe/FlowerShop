using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-don-hang")]
	public class SaleInvoiceController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISaleInvoiceService _saleInvoiceService;

		public SaleInvoiceController(FlowerShopContext context, ISaleInvoiceService saleInvoiceService)
		{
			_context = context;
			_saleInvoiceService = saleInvoiceService;
		}

		// GET: Admin/SaleInvoice
		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var saleInvoice = await _saleInvoiceService.GetSaleInvoiceWithIcludeAsync();
			var saleInvoiceListVM = new List<SaleInvoiceViewModel>();
			foreach (var item in saleInvoice)
			{
				saleInvoiceListVM.Add(new SaleInvoiceViewModel()
				{
					Id = item.Id,
					CreateDay = item.CreateDay,
					Customer = item.Customer,
					CustomerId = item.CustomerId,
					PaymentMethod = item.PaymentMethod,
					Status = item.Status,
					IsDelete = item.IsDelete,
				});
			}
			return View(saleInvoice);
		}

		[HttpGet("da-xac-nhan")]
		public async Task<IActionResult> OrderConfirmed()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangDaXacNhan();
			return View(saleInvoice);
		}

		[HttpGet("da-huy")]
		public async Task<IActionResult> OrderCancelled()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangDaHuy();
			return View(saleInvoice);
		}

		[HttpGet("dang-chuan-bi")]
		public async Task<IActionResult> OrderPrepared()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangDangChuanBi();
			return View(saleInvoice);
		}

		[HttpGet("dang-giao-hang")]
		public async Task<IActionResult> OrderDelivery()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangDangGiao();
			return View(saleInvoice);
		}

		[HttpGet("don-hang-giao-thanh-cong")]
		public async Task<IActionResult> SuccessDeliveryOrder()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangGiaoThanhCong();
			return View(saleInvoice);
		}

		[HttpGet("don-hang-giao-khong-thanh-cong")]
		public async Task<IActionResult> FailDeliveryOrder()
		{
			var saleInvoice = await _saleInvoiceService.LayCacDonHangGiaoThatBai();
			return View(saleInvoice);
		}

		private bool SaleInvoiceExists(int id)
		{
			return (_context.SaleInvoices?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}