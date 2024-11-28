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
using FlowerShop.Common.Template;
using Microsoft.AspNetCore.Hosting;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-nha-cung-cap")]
	public class SuppliersController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ISuppliersService _supplierService;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public SuppliersController(FlowerShopContext context, ISuppliersService suppliersService, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_supplierService = suppliersService;
			_webHostEnvironment = webHostEnvironment;
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

		[HttpGet("create")]
		public IActionResult Create()
		{
			return View(new SupplierViewModel());
		}

		// POST: Admin/Suppliers/Create
		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CompanyName,TaxCode,Email,Phone,Type,Images,Description,Industry,Address,IsDelete")] SupplierViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Nếu có ảnh được chọn
				if (Request.Form.Files.Count > 0)
				{
					var file = Request.Form.Files[0]; // Lấy file đầu tiên (nếu có)

					// Tạo tên file
					var fileName = Path.GetFileName(file.FileName);

					// Tạo đường dẫn lưu file vào thư mục
					var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "suppliers", fileName);

					// Lưu file vào thư mục
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);  // Lưu file vào thư mục
					}
					// Lưu tên file vào model (không lưu file lên server)
					model.Images = fileName;
				}

				// Lưu thông tin nhà cung cấp vào cơ sở dữ liệu
				var supplier = new Supplier
				{
					CompanyName = model.CompanyName,
					TaxCode = model.TaxCode,
					Email = model.Email,
					Phone = model.Phone,
					Description = model.Description,
					Address = model.Address,
					Images = model.Images, // Lưu tên file vào DB
					IsDelete = model.IsDelete
				};
				var result = _supplierService.AddNewSupplier(supplier);
				if (result != null)
				{
					TempData["SuccessMessage"] = "Nhà cung cấp đã được thêm thành công!";
				}
				else
				{
					TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm nhà cung cấp.";
				}
			}
			return View(model);  // Trả về lại view nếu có lỗi
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

			// Giải mã chuỗi JSON của Images nếu nó là một chuỗi JSON
			string image = string.Empty;
			if (!string.IsNullOrEmpty(supplier.Images))
			{
				try
				{
					// Loại bỏ escape và dấu ngoặc vuông
					image = supplier.Images.Replace("\"", "").Trim('[', ']');
				}
				catch (Exception ex)
				{
					image = string.Empty;
					Console.WriteLine("Error while deserializing image JSON: " + ex.Message);
				}
			}

			var supplierVM = new SupplierViewModel()
			{
				Id = supplier.Id,
				CompanyName = supplier.CompanyName,
				TaxCode = supplier.TaxCode,
				Type = supplier.Type,
				Images = image,  // Chỉ lưu một ảnh duy nhất
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
				// Nếu có ảnh được chọn
				if (Request.Form.Files.Count > 0)
				{
					var file = Request.Form.Files[0]; // Lấy file đầu tiên (nếu có)

					// Tạo tên file
					var fileName = Path.GetFileName(file.FileName);

					// Tạo đường dẫn lưu file vào thư mục
					var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "suppliers", fileName);

					// Lưu file vào thư mục
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);  // Lưu file vào thư mục
					}
					// Lưu tên file vào model (không lưu file lên server)
					supplierVM.Images = fileName;
				}
				else
				{
					string image = string.Empty;
					if (!string.IsNullOrEmpty(supplier.Images))
					{
						try
						{
							// Loại bỏ escape và dấu ngoặc vuông
							image = supplier.Images.Replace("\"", "").Trim('[', ']');
						}
						catch (Exception ex)
						{
							image = string.Empty;
							Console.WriteLine("Error while deserializing image JSON: " + ex.Message);
						}
					}
					supplierVM.Images = image;  // Giữ lại tên ảnh cũ
				}
				try
				{
					supplier.CompanyName = supplierVM.CompanyName;
					supplier.TaxCode = supplierVM.TaxCode;
					supplier.Images = supplierVM.Images;
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

		[HttpPost("delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var supplier = await _supplierService.GetSingleById(id);
			if (supplier == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}
			await _supplierService.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool SupplierExists(int id)
		{
			return (_context.Suppliers?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}