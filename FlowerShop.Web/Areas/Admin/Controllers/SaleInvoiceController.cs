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

		// GET: Admin/SaleInvoice/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.SaleInvoices == null)
			{
				return NotFound();
			}

			var saleInvoice = await _context.SaleInvoices
				.Include(s => s.Customer)
				.Include(s => s.PaymentMethod)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (saleInvoice == null)
			{
				return NotFound();
			}

			return View(saleInvoice);
		}

		// GET: Admin/SaleInvoice/Create
		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_context.AppUsers, "Id", "Id");
			ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name");
			return View();
		}

		// POST: Admin/SaleInvoice/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CreateDay,CustomerId,PaymentMethodId,Status,IsDelete")] SaleInvoice saleInvoice)
		{
			if (ModelState.IsValid)
			{
				_context.Add(saleInvoice);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CustomerId"] = new SelectList(_context.AppUsers, "Id", "Id", saleInvoice.CustomerId);
			ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", saleInvoice.PaymentMethodId);
			return View(saleInvoice);
		}

		// GET: Admin/SaleInvoice/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.SaleInvoices == null)
			{
				return NotFound();
			}

			var saleInvoice = await _context.SaleInvoices.FindAsync(id);
			if (saleInvoice == null)
			{
				return NotFound();
			}
			ViewData["CustomerId"] = new SelectList(_context.AppUsers, "Id", "Id", saleInvoice.CustomerId);
			ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", saleInvoice.PaymentMethodId);
			return View(saleInvoice);
		}

		// POST: Admin/SaleInvoice/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CreateDay,CustomerId,PaymentMethodId,Status,IsDelete")] SaleInvoice saleInvoice)
		{
			if (id != saleInvoice.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(saleInvoice);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SaleInvoiceExists(saleInvoice.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["CustomerId"] = new SelectList(_context.AppUsers, "Id", "Id", saleInvoice.CustomerId);
			ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", saleInvoice.PaymentMethodId);
			return View(saleInvoice);
		}

		// GET: Admin/SaleInvoice/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.SaleInvoices == null)
			{
				return NotFound();
			}

			var saleInvoice = await _context.SaleInvoices
				.Include(s => s.Customer)
				.Include(s => s.PaymentMethod)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (saleInvoice == null)
			{
				return NotFound();
			}

			return View(saleInvoice);
		}

		// POST: Admin/SaleInvoice/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.SaleInvoices == null)
			{
				return Problem("Entity set 'FlowerShopContext.SaleInvoices'  is null.");
			}
			var saleInvoice = await _context.SaleInvoices.FindAsync(id);
			if (saleInvoice != null)
			{
				_context.SaleInvoices.Remove(saleInvoice);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SaleInvoiceExists(int id)
		{
			return (_context.SaleInvoices?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}