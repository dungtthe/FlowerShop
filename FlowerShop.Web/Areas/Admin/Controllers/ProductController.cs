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
using AutoMapper;
using FlowerShop.Common.ViewModels;
using Newtonsoft.Json;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-li-sp")]
    public class ProductController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IProductService _productService;
        private readonly IProductItemService _productItemService;
        private readonly ICategoryService _categoryService;
        private readonly IPackagingService _packagingService;
        private readonly IMapper _mapper;

        public ProductController(FlowerShopContext context, IProductService productService,IMapper mapper,IProductItemService productItemService,ICategoryService categoryService,IPackagingService packagingService)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
            _productItemService = productItemService;
            _categoryService = categoryService;
            _packagingService = packagingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsForIndexAsync();
            if (products == null)
            {
                return NotFound();
            }

            var productsVM = new List<ProductViewModel>();
            foreach(var item in products)
            {
                var productVM = _mapper.Map<ProductViewModel>(item);
                productsVM.Add(productVM);
            }
            return View(productsVM);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var productsItem = await _productItemService.GetProductsItemAsync();
            var categories = await _categoryService.GetCategoriesWithoutSubCategories();
            var packagings=await _packagingService.GetAllPackagingAsync();
            if (productsItem == null|| categories==null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Có lỗi xảy ra!"));
                return RedirectToAction("Index");   
            }
            ViewBag.ProductsItem =productsItem;
            ViewBag.categories = categories; 
            ViewBag.packagings = new SelectList(packagings, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Images,PackagingId,IsDelete")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackagingId"] = new SelectList(_context.Packagings, "Id", "Name", product.PackagingId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["PackagingId"] = new SelectList(_context.Packagings, "Id", "Name", product.PackagingId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Images,PackagingId,IsDelete")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["PackagingId"] = new SelectList(_context.Packagings, "Id", "Name", product.PackagingId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'FlowerShopContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
