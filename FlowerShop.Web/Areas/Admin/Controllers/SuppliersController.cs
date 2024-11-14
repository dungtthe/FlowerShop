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
using FlowerShop.Common.MyConst;
using static System.Net.Mime.MediaTypeNames;
using FlowerShop.Common.ViewModels;
using Newtonsoft.Json;
using System.Security.Cryptography;

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
			var supplierList = await _supplierService.GetSuppliersAsync();
			var supplierListVM = new List<SupplierViewModel>();
			foreach (var item in supplierList)
			{
				supplierListVM.Add(new SupplierViewModel()
				{
					Id = item.Id,
					CompanyName = item.CompanyName,
					Email = item.Email,
					Phone = item.Phone,
					Description = item.Description,
				});
			}
			return View(supplierList);
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
		[HttpGet("edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			var supplier = await _supplierService.GetSingleById(id ?? -1);
			if (supplier == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}
			var supplierVM = new SupplierViewModel()
			{
				Id = supplier.Id,
				CompanyName = supplier.CompanyName,
				TaxCode = supplier.TaxCode,
				Type = supplier.Type,
				Images = supplier.Images,
				Industry = supplier.Industry,
				Address = supplier.Address,
				IsDelete = supplier.IsDelete,
				Email = supplier.Email,
				Phone = supplier.Phone,
				Description = supplier.Description,
			};

			return View(supplierVM);
		}

		// POST: Admin/Suppliers/Edit/5
		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, [Bind("Id,CompanyName,TaxCode,Email,Phone,Type,Images,Description,Industry,Address,IsDelete")] SupplierViewModel supplierVM)
		{
			var supplier = await _supplierService.GetSingleById(id ?? -1);
			if (supplier == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}

			if (ModelState.IsValid)
			{
				try
				{
					supplier.CompanyName = supplierVM.CompanyName;
					supplier.TaxCode = supplierVM.TaxCode;
					supplier.Type = supplierVM.Type;
					supplier.Images = supplierVM.Images;
					supplier.Industry = supplierVM?.Industry;
					supplier.Address = supplierVM?.Address;
					supplier.IsDelete = supplierVM.IsDelete;
					supplier.Email = supplierVM.Email;
					supplier.Phone = supplierVM.Phone;
					supplier.Description = supplierVM.Description;

					var result = _supplierService.UpdateAsync(supplier);
					if (result == null)
					{
						return Content(ConstValues.CoLoiXayRa);
					}
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

		[HttpPost("delete/{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var supplier = await _supplierService.GetSingleById(id);
			if (supplier == null)
			{
				TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy danh mục để xóa"));
			}
			PopupViewModel rsp = await _supplierService.Delete(id);
			TempData["PopupViewModel"] = JsonConvert.SerializeObject(rsp);
			return RedirectToAction("Index", "Suppliers", new { area = "ADMIN" });
		}

		private bool SupplierExists(int id)
		{
			return (_context.Suppliers?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}