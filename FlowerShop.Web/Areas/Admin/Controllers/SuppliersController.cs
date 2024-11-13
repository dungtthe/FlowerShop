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

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-nha-cung-cap")]
	public class SuppliersController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISuppliersService _supplierService;

		public SuppliersController(FlowerShopContext context, ISuppliersService suppliersService)
		{
			_context = context;
			_supplierService = suppliersService;
		}

		// GET: Admin/Suppliers
		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			/*return _context.Suppliers != null ?
                        View(await _context.Suppliers.ToListAsync()) :
                        Problem("Entity set 'FlowerShopContext.Suppliers'  is null.");*/
			var result = await _supplierService.GetSuppliersAsync();
			return View(result);
		}

		// GET: Admin/Suppliers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Suppliers == null)
			{
				return NotFound();
			}

			var supplier = await _context.Suppliers
				.FirstOrDefaultAsync(m => m.Id == id);
			if (supplier == null)
			{
				return NotFound();
			}

			return View(supplier);
		}

		// GET: Admin/Suppliers/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Admin/Suppliers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CompanyName,TaxCode,Email,Phone,Type,Images,Description,Industry,Address,IsDelete")] Supplier supplier)
		{
			if (ModelState.IsValid)
			{
				_context.Add(supplier);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(supplier);
		}

		// GET: Admin/Suppliers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Suppliers == null)
			{
				return NotFound();
			}

			var supplier = await _context.Suppliers.FindAsync(id);
			if (supplier == null)
			{
				return NotFound();
			}
			return View(supplier);
		}

		// POST: Admin/Suppliers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,TaxCode,Email,Phone,Type,Images,Description,Industry,Address,IsDelete")] Supplier supplier)
		{
			if (id != supplier.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(supplier);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SupplierExists(supplier.Id))
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
			return View(supplier);
		}

		// GET: Admin/Suppliers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Suppliers == null)
			{
				return NotFound();
			}

			var supplier = await _context.Suppliers
				.FirstOrDefaultAsync(m => m.Id == id);
			if (supplier == null)
			{
				return NotFound();
			}

			return View(supplier);
		}

		// POST: Admin/Suppliers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Suppliers == null)
			{
				return Problem("Entity set 'FlowerShopContext.Suppliers'  is null.");
			}
			var supplier = await _context.Suppliers.FindAsync(id);
			if (supplier != null)
			{
				_context.Suppliers.Remove(supplier);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SupplierExists(int id)
		{
			return (_context.Suppliers?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}