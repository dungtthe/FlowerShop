using FlowerShop.Common.Template;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
	[Area("CUSTOMER")]
	[Route("quan-ly-lich-su-mua-hang")]
	[Authorize]
	public class PurchaseHistoryController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISaleInvoiceService _saleInvoiceService;
		private readonly UserManager<AppUser> _userManager;

		public PurchaseHistoryController(FlowerShopContext context, ISaleInvoiceService saleInvoiceService, UserManager<AppUser> userManager)
		{
			_context = context;
			_saleInvoiceService = saleInvoiceService;
			_userManager = userManager;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			ViewBag.tilePage = "Đơn hàng chờ xác nhận";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangChoXacNhanCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("da-xac-nhan")]
		public async Task<IActionResult> DaXacNhan()
		{
			ViewBag.tilePage = "Đơn hàng đã xác nhận";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangDaXacNhanCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("dang-chuan-bi")]
		public async Task<IActionResult> DangChuanBi()
		{
			ViewBag.tilePage = "Đơn hàng đang chuẩn bị";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangDangChuanBiCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("dang-giao")]
		public async Task<IActionResult> DangGiao()
		{
			ViewBag.tilePage = "Đơn hàng đang giao";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangDangGiaoCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("giao-thanh-cong")]
		public async Task<IActionResult> GiaoThanhCong()
		{
			ViewBag.tilePage = "Đơn hàng giao thành công";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangGiaoThanhCongCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("giao-that-bai")]
		public async Task<IActionResult> GiaoThatBai()
		{
			ViewBag.tilePage = "Đơn hàng giao thất bại";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangGiaoThatBaiCuaNguoiDung(userId);
			return View(saleInvoices);
		}

		[HttpGet("da-huy")]
		public async Task<IActionResult> DaHuy()
		{
			ViewBag.tilePage = "Đơn hàng đã hủy";
			var userId = _userManager.GetUserId(User);
			var saleInvoices = await _saleInvoiceService.LayCacDonHangDaHuyCuaNguoiDung(userId);
			return View(saleInvoices);
		}
	}
}