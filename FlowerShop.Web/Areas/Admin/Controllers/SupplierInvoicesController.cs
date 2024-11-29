using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-ly-don-nhap")]
	public class SupplierInvoicesController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISupplierInvoiceService _supplierInvoiceService;

		public SupplierInvoicesController(FlowerShopContext context, ISupplierInvoiceService supplierInvoiceService)
		{
			_context = context;
			_supplierInvoiceService = supplierInvoiceService;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var supplierInvoices = await _supplierInvoiceService.GetSuppliersInvoiceAsync();

			var invoiceTotals = new Dictionary<int, float>();

			foreach (var invoice in supplierInvoices)
			{
				// Tính tổng tiền cho từng hóa đơn
				var totalAmount = await _supplierInvoiceService.GetTongTien(invoice.Id);
				invoiceTotals[invoice.Id] = totalAmount;
			}

			ViewData["InvoiceTotals"] = invoiceTotals;

			return View(supplierInvoices);
		}

		[HttpGet("hoa-don-nhap-da-huy")]
		public async Task<IActionResult> CancelledSupplierInvoice()
		{
			var supplierInvoices = await _supplierInvoiceService.GetCancelledSupplierInvoice();

			var invoiceTotals = new Dictionary<int, float>();

			foreach (var invoice in supplierInvoices)
			{
				// Tính tổng tiền cho từng hóa đơn
				var totalAmount = await _supplierInvoiceService.GetTongTien(invoice.Id);
				invoiceTotals[invoice.Id] = totalAmount;
			}

			ViewData["InvoiceTotals"] = invoiceTotals;

			return View(supplierInvoices);
		}

		[HttpGet("hoa-don-nhap-nhap-thanh-cong")]
		public async Task<IActionResult> SuccessSupplierInvoice()
		{
			var supplierInvoices = await _supplierInvoiceService.GetSuccessSupplierInvoice();

			var invoiceTotals = new Dictionary<int, float>();

			foreach (var invoice in supplierInvoices)
			{
				// Tính tổng tiền cho từng hóa đơn
				var totalAmount = await _supplierInvoiceService.GetTongTien(invoice.Id);
				invoiceTotals[invoice.Id] = totalAmount;
			}

			ViewData["InvoiceTotals"] = invoiceTotals;

			return View(supplierInvoices);
		}

		private bool SupplierInvoiceExists(int id)
		{
			return (_context.SupplierInvoices?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}