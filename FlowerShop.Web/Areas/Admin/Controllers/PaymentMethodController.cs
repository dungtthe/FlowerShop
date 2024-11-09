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
using FlowerShop.Common;
using FlowerShop.Web.ViewModels;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/quan-li-thanh-toan")]
    public class PaymentMethodController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(FlowerShopContext context, IPaymentMethodService paymentMethodService)
        {
            _context = context;
            _paymentMethodService = paymentMethodService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PaymentMethod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Status,IsDelete")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
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



                    //var result = _paymentMethodService.Update(paymentMethod);

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

            return RedirectToAction("Index","PaymentMethod",new {area = "Admin"});
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
