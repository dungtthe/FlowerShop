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

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/quan-li-kho")]
    public class ProductItemController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IProductItemService _productItemService;

        public ProductItemController(FlowerShopContext context,IProductItemService productItemService)
        {
            _context = context;
            _productItemService = productItemService;
        }

        // GET: Admin/ProductItem
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //var flowerShopContext = _context.ProductItems.Include(p => p.Category);
            var flowerShopContext = await _productItemService.GetProductsAsync();
            return View(flowerShopContext);
        }

        // GET: Admin/ProductItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // GET: Admin/ProductItem/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImportPrice,CategoryId,Images,Description,IsDelete")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItem.CategoryId);
            return View(productItem);
        }

        // GET: Admin/ProductItem/Edit/5
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItem.CategoryId);
            return View(productItem);
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImportPrice,CategoryId,Images,Description,IsDelete")] ProductItem productItem)
        {
            if (id != productItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(productItem);
                    //await _context.SaveChangesAsync();
                    var result= await _productItemService.UpdateAsync(productItem);
                    if (result == null)
                    {
                        return Content(ConstValues.CoLoiXayRa);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItem.CategoryId);
            return View(productItem);
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductItems == null)
            {
                return Problem("Entity set 'FlowerShopContext.ProductItems'  is null.");
            }
            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem != null)
            {
                _context.ProductItems.Remove(productItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
          return (_context.ProductItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
