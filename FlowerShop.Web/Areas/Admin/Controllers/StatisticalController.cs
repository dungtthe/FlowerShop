using FlowerShop.DataAccess;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/thong-ke")]
	public class StatisticalController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISupplierInvoiceService _supplierInvoiceService;
		private readonly ISaleInvoiceService _saleInvoiceService;

		public StatisticalController(FlowerShopContext context, ISupplierInvoiceService supplierInvoiceService, ISaleInvoiceService saleInvoiceService)
		{
			_context = context;
			_supplierInvoiceService = supplierInvoiceService;
			_saleInvoiceService = saleInvoiceService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var tongDoanhThuThangNay = await _saleInvoiceService.TongDoanhThuThangNay();
			var tongChiThangNay = await _supplierInvoiceService.TongChiThangNay();
			var tongDoanhThuHomNay = await _saleInvoiceService.TongDoanhThuHomNay();
			var soDonHangCho = await _saleInvoiceService.SoDonHangCho();

			// Truyền dữ liệu sang View
			if (tongDoanhThuThangNay == 0)
			{
				ViewBag.TongDoanhThuThangNay = 0;
			}
			else
			{
				ViewBag.TongDoanhThuThangNay = string.Format("{0:0,0}", tongDoanhThuThangNay).Replace(",", ".");
			}

			if (tongDoanhThuHomNay == 0)
			{
				ViewBag.TongDoanhThuHomNay = 0;
			}
			else
			{
				ViewBag.TongDoanhThuHomNay = string.Format("{0:0,0}", tongDoanhThuHomNay).Replace(",", ".");
			}

			if (tongChiThangNay == 0)
			{
				ViewBag.TongChiThangNay = 0;
			}
			else
			{
				ViewBag.TongChiThangNay = string.Format("{0:0,0}", tongChiThangNay).Replace(",", ".");
			}
			ViewBag.SoDonHangCho = soDonHangCho;

			return View();
		}

		[HttpGet("top-san-pham")]
		public async Task<IActionResult> TopSanPham()
		{
			var tongDoanhThuThangNay = await _saleInvoiceService.TongDoanhThuThangNay();
			var tongChiThangNay = await _supplierInvoiceService.TongChiThangNay();
			var tongDoanhThuHomNay = await _saleInvoiceService.TongDoanhThuHomNay();
			var soDonHangCho = await _saleInvoiceService.SoDonHangCho();

			// Truyền dữ liệu sang View
			if (tongDoanhThuThangNay == 0)
			{
				ViewBag.TongDoanhThuThangNay = 0;
			}
			else
			{
				ViewBag.TongDoanhThuThangNay = string.Format("{0:0,0}", tongDoanhThuThangNay).Replace(",", ".");
			}

			if (tongDoanhThuHomNay == 0)
			{
				ViewBag.TongDoanhThuHomNay = 0;
			}
			else
			{
				ViewBag.TongDoanhThuHomNay = string.Format("{0:0,0}", tongDoanhThuHomNay).Replace(",", ".");
			}

			if (tongChiThangNay == 0)
			{
				ViewBag.TongChiThangNay = 0;
			}
			else
			{
				ViewBag.TongChiThangNay = string.Format("{0:0,0}", tongChiThangNay).Replace(",", ".");
			}
			ViewBag.SoDonHangCho = soDonHangCho;

			return View();
		}
	}
}