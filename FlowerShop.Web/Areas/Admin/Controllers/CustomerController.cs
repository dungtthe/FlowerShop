using FlowerShop.DataAccess;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-khach-hang")]
	public class CustomerController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly ICustomerService _customerService;

		public CustomerController(FlowerShopContext context, ICustomerService customerService)
		{
			_context = context;
			_customerService = customerService;
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
					FullName = customer.FullName,
					BirthDay = customer.BirthDay,
					PhoneNumber = customer.PhoneNumber,
					IsDelete = customer.IsDelete,
					IsLock = customer.IsLock,
				});
			}
			return View(customerListVM);
		}
	}
}