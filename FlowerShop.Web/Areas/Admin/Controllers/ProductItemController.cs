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
using FlowerShop.Common.Helpers;
using FlowerShop.Common.MyConst;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-li-kho")]
    public class ProductItemController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IProductItemService _productItemService;
        private readonly IProductProductItemService _productProductItemService;
        public ProductItemController(FlowerShopContext context,IProductItemService productItemService, IProductProductItemService productProductItemService)
        {
            _context = context;
            _productItemService = productItemService;
            _productProductItemService = productProductItemService;
        }

        // GET: Admin/ProductItem
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {




            //var flowerShopContext = _context.ProductItems.Include(p => p.Category);
            var productItems = await _productItemService.GetProductsItemAsync();

            var productItemsVM= new List<ProductItemViewModel>();
            foreach(var item in productItems)
            {
                productItemsVM.Add(new ProductItemViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImportPrice = item.ImportPrice,
                    CategoryId = item.CategoryId,
                    Category=item.Category,
                    Images = item.Images,
                    Description = item.Description,
                    IsDelete = item.IsDelete,
                    ProductProductItems=item.ProductProductItems
                });
            }

            return View(productItemsVM);
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
            var productItem = await _productItemService.GetSingleById(id ?? -1);
            if(productItem == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            var productItemVM= new ProductItemViewModel(){
                Id=productItem.Id,
                Name=productItem.Name,
                ImportPrice=productItem.ImportPrice,
                Images=productItem.Images,
                CategoryId=productItem.CategoryId,
                Category=productItem.Category,
                Description=productItem.Description,
            };

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItemVM.CategoryId);
            return View(productItemVM);
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ?id, [Bind("Name,CategoryId,Description")] ProductItemViewModel productItemVM)
        {
            var productItem = await _productItemService.GetSingleById(id ?? -1);
            if (productItem == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            if (ModelState.IsValid)
            {
                try
                {

                    productItem.Name= productItemVM.Name;
                    productItem.CategoryId= productItemVM.CategoryId;
                    //productItem.Images= productItemVM.Images;
                    productItem.Description= productItemVM.Description;
                    


                    var result= await _productItemService.UpdateAsync(productItem);
                    if (result == null)
                    {
                        return Content(ConstValues.CoLoiXayRa);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItemVM.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItemVM.CategoryId);
            return View(productItemVM);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _productItemService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            var checkExist = await _productProductItemService.CheckExistPrductItem(product.Id);
            if (checkExist)
            {
                return Content("Sản phẩm này đang được bán nên không thể xóa!");
            }
            await _productItemService.DeleteAsync(id??-1);

            return RedirectToAction(nameof(Index));
        }


        private bool ProductItemExists(int id)
        {
          return (_context.ProductItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [HttpGet("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(int ?id)
        {
            var product = await _productItemService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            ViewData["product"] = product;
            return View(new UploadOneFile());
        }


        [HttpPost("uploadphoto"), ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int ?id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = await _productItemService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            if (f != null && f.FileUpload!=null)
            {
                // fileName random
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                     + Path.GetExtension(f.FileUpload.FileName);
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                var filePath = Path.Combine(wwwRootPath, fileName);

                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }
                var imgs = Utils.AddPhotoForProduct(fileName, product.Images);
                product.Images = imgs;
                await _productItemService.UpdateAsync(product);
            }
            return RedirectToAction(nameof(Edit), new { id = id });
        }



       
    }
}
