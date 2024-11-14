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
using AutoMapper;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-li-kho")]
    public class ProductItemController : Controller
    {
        private readonly IProductItemService _productItemService;
        private readonly IProductProductItemService _productProductItemService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductItemController(IProductItemService productItemService, IProductProductItemService productProductItemService, ICategoryService categoryService, IMapper mapper)
        {
            _productItemService = productItemService;
            _productProductItemService = productProductItemService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var productItems = await _productItemService.GetProductsItemAsync();

            var productItemsVM = new List<ProductItemViewModel>();
            foreach (var item in productItems)
            {
                var productItem = _mapper.Map<ProductItemViewModel>(item);
                productItemsVM.Add(productItem);
            }

            return View(productItemsVM);
        }


        public IActionResult Create()
        {
            // ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImportPrice,CategoryId,Images,Description,IsDelete")] ProductItem productItem)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(productItem);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productItem.CategoryId);
            return View(productItem);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var productItem = await _productItemService.GetSingleById(id ?? -1);
            if (productItem == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            var productItemVM = _mapper.Map<ProductItemViewModel>(productItem);

            var categories = await _categoryService.GetAllCategoriesNotWithHierarchy();

            ViewData["Categories"] = new SelectList(categories, "Id", "Name", productItemVM.CategoryId);
            return View(productItemVM);
        }
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Name,CategoryId,Description")] ProductItemViewModel productItemVM)
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

                    productItem.Name = productItemVM.Name;
                    productItem.CategoryId = productItemVM.CategoryId;
                    //productItem.Images= productItemVM.Images;
                    productItem.Description = productItemVM.Description;



                    var result = await _productItemService.UpdateAsync(productItem);
                    if (result == null)
                    {
                        return Content(ConstValues.CoLoiXayRa);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllCategoriesNotWithHierarchy();
            ViewData["Categories"] = new SelectList(categories, "Id", "Name", productItemVM.CategoryId);
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
            await _productItemService.DeleteAsync(id ?? -1);

            return RedirectToAction(nameof(Index));
        }




        [HttpGet("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(int? id)
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
        public async Task<IActionResult> UploadPhotoAsync(int? id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = await _productItemService.GetSingleById(id ?? -1);
            if (product == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            if (f != null && f.FileUpload != null)
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
