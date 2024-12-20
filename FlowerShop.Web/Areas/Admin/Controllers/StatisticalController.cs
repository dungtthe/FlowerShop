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

			// Truyền dữ liệu sang View nếu cần
			ViewBag.TongDoanhThuThangNay = string.Format("{0:0,0}", tongDoanhThuThangNay).Replace(",", ".");

			return View();
		}
	}
}