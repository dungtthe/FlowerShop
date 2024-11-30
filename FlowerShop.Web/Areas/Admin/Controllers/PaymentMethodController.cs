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
using FlowerShop.Web.ViewModels;
using FlowerShop.Common.MyConst;
using FlowerShop.Service.ServiceImpl;
using AutoMapper;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-li-thanh-toan")]
    public class PaymentMethodController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IPaymentMethodService _paymentMethodService;
		private readonly IMapper _mapper;
		public PaymentMethodController(FlowerShopContext context, IPaymentMethodService paymentMethodService, IMapper mapper)
        {
            _context = context;
            _paymentMethodService = paymentMethodService;
			_mapper = mapper;
		}

        // GET: Admin/PaymentMethod
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //return _context.PaymentMethods != null ? 
            //            View(await _context.PaymentMethods.ToListAsync()) :
            //            Problem("Entity set 'FlowerShopContext.PaymentMethods'  is null.");

            var result = await _paymentMethodService.GetPaymentMethodsAsync();
            return View(result);

        }

        // GET: Admin/PaymentMethod/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

		// GET: Admin/PaymentMethod/Create
		[HttpGet("create")]
		public IActionResult Create()
        {
            return View();
        }
		// POST: Admin/PaymentMethod/Create
		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Description,Status")] PaymentMethodViewModel paymentMethodVM)
		{
			// Kiểm tra trùng lặp tên phương thức thanh toán
			var allPaymentMethod = await _paymentMethodService.GetPaymentMethodsAsync();
			var isNameDuplicate = allPaymentMethod.Any(item => item.Name.Equals(paymentMethodVM.Name, StringComparison.OrdinalIgnoreCase));

			if (isNameDuplicate)
			{
                ModelState.AddModelError("Name", "Tên phương thức thanh toán đã tồn tại. Vui lòng nhập tên khác.");
			}
            // Kiểm tra giá trị Price và gán 0 nếu nó là null hoặc không hợp lệ
            if (paymentMethodVM.Price == null || paymentMethodVM.Price <= 0)
            {
                paymentMethodVM.Price = 0;
            }

            if (ModelState.IsValid)
			{
				// Chuyển đổi ViewModel thành Entity
				var payment = _mapper.Map<PaymentMethod>(paymentMethodVM);

				// Thêm mới phương thức thanh toán
				var result = await _paymentMethodService.AddAsync(payment);
				if (result != null)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return Content(ConstValues.CoLoiXayRa);
				}
			}

			// Nếu có lỗi, trả về lại trang với dữ liệu cũ
			return View(paymentMethodVM);
		}

		

        // GET: Admin/PaymentMethod/Edit/5
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }



            var viewModel = new PaymentMethodViewModel()
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
                Description = paymentMethod.Description,
                Price = paymentMethod.Price,
                Status = paymentMethod.Status,
                IsDelete = paymentMethod.IsDelete,
            };



            return View(viewModel);
        }

        // POST: Admin/PaymentMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Status,IsDelete")] PaymentMethodViewModel paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(paymentMethod);
                    //await _context.SaveChangesAsync();



                    //var result = await _paymentMethodService.Update(paymentMethod);

                    //if (result == null)
                    //{
                    //    return Content(ConstValues.CoLoiXayRa);
                    //}




                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.Id))
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
            return View(paymentMethod);
        }

        // GET: Admin/PaymentMethod/Delete/5
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var result = await _paymentMethodService.DeleteAsync(id);

            if (!result)
            {
                return Content(ConstValues.XoaThatBai);
            }

            return RedirectToAction("Index","PaymentMethod",new {area = "ADMIN"});
        }

        //// POST: Admin/PaymentMethod/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.PaymentMethods == null)
        //    {
        //        return Problem("Entity set 'FlowerShopContext.PaymentMethods'  is null.");
        //    }
        //    var paymentMethod = await _context.PaymentMethods.FindAsync(id);
        //    if (paymentMethod != null)
        //    {
        //        _context.PaymentMethods.Remove(paymentMethod);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PaymentMethodExists(int id)
        {
            return (_context.PaymentMethods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
