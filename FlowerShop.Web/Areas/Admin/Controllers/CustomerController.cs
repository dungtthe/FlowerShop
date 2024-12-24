using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-khach-hang")]
	public class CustomerController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ICustomerService _customerService;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public CustomerController(FlowerShopContext context, ICustomerService customerService, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_customerService = customerService;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var customerList = await _customerService.GetCustomerAsync();
			var customerListVM = new List<CustomerViewModel>();
			foreach (var customer in customerList)
			{
				customerListVM.Add(new CustomerViewModel
				{
					Id = customer.Id,
					FullName = customer.FullName,
					BirthDay = customer.BirthDay,
					PhoneNumber = customer.PhoneNumber,
					IsDelete = customer.IsDelete,
					IsLock = customer.IsLock,
				});
			}
			return View(customerList);
		}

		[HttpGet("edit")]
		public async Task<IActionResult> Edit(string? id)
		{
			var customer = await _customerService.GetSingleById(id ?? "-1");
			if (customer == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}

			// Giải mã chuỗi JSON của Images nếu nó là một chuỗi JSON
			string image = string.Empty;
			if (!string.IsNullOrEmpty(customer.Images))
			{
				try
				{
					// Loại bỏ escape và dấu ngoặc vuông
					image = customer.Images.Replace("\"", "").Trim('[', ']');
				}
				catch (Exception ex)
				{
					image = string.Empty;
				}
			}

			var customerViewModel = new CustomerViewModel()
			{
				Id = customer.Id,
				FullName = customer.FullName,
				BirthDay = customer.BirthDay,
				Images = image,
				PhoneNumber = customer.PhoneNumber,
				IsDelete = customer.IsDelete,
				IsLock = customer.IsLock,
				Email = customer.Email,
			};

			return View(customerViewModel);
		}

		// POST: Admin/Suppliers/Edit/5
		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string? id, [Bind("Id,FullName,BirthDay,Images,IsLock,IsDelete,Email,PhoneNumber")] CustomerViewModel customerVM)
		{
			var customer = await _customerService.GetSingleById(id ?? "-1");
			if (customer == null)
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
					var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "users", fileName);

					// Lưu file vào thư mục
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);  // Lưu file vào thư mục
					}
					// Lưu tên file vào model (không lưu file lên server)
					customerVM.Images = fileName;
				}
				else
				{
					string image = string.Empty;
					if (!string.IsNullOrEmpty(customer.Images))
					{
						try
						{
							// Loại bỏ escape và dấu ngoặc vuông
							image = customer.Images.Replace("\"", "").Trim('[', ']');
						}
						catch (Exception ex)
						{
							image = string.Empty;
							Console.WriteLine("Error while deserializing image JSON: " + ex.Message);
						}
					}
					customerVM.Images = image;  // Giữ lại tên ảnh cũ
				}
				try
				{
					customer.FullName = customerVM.FullName;
					customer.BirthDay = customerVM.BirthDay;
					customer.Images = customerVM.Images;
					customer.PhoneNumber = customerVM.PhoneNumber;
					customer.IsDelete = customerVM.IsDelete;
					customer.IsLock = customerVM.IsLock;
					customer.Email = customerVM.Email;
					customer.Images = customerVM.Images;
					var result = await _customerService.UpdateAsync(customer);
					if (result == null)
					{
						return Content(ConstValues.CoLoiXayRa);
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CustomerExists(customer.Id))
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
			return View(customer);
		}

		[HttpPost("delete")]
		public async Task<IActionResult> Delete(string id)
		{
			var customer = await _customerService.GetSingleById(id);
			if (customer == null)
			{
				TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy khách hàng để xóa"));
			}
			PopupViewModel rsp = await _customerService.Delete(customer);
			TempData["PopupViewModel"] = JsonConvert.SerializeObject(rsp);
			return RedirectToAction("Index", "Customer", new { area = "ADMIN" });
		}

		private bool CustomerExists(string id)
		{
			return _context.AppUsers?.Any(e => e.Id == id) ?? false;
		}
	}
}