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
using FlowerShop.Common.ViewModels;
using Newtonsoft.Json;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.Mappings;

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

        [HttpGet("nhap-kho")]
        public async Task<IActionResult> Create()
        {
            var productsItem = await _productItemService.GetProductsItemAsync();
            var categories = (await _categoryService.GetCategoriesWithoutSubCategories()).Where(c =>!c.IsCategorySell);

            if (categories == null || productsItem == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Có lỗi xảy ra!"));
                return RedirectToAction("Index");
            }
            ViewBag.productItemInStock = productsItem;    
            ViewBag.categoryInStock = categories;   
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
            var productItem = await _productItemService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (productItem == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction("Index");
            }
            var productItemVM = _mapper.Map<ProductItemViewModel>(productItem);
            var categories = (await _categoryService.GetCategoriesWithoutSubCategories());
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productItemVM.CategoryId);
            return View(productItemVM);
        }


        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Name,CategoryId,Description")] ProductItemViewModel productItemVM)
        {
            var productItem = await _productItemService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (productItem == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                productItem.Name = productItemVM.Name;
                productItem.CategoryId = productItemVM.CategoryId;
                productItem.Description = productItemVM.Description;
                await _productItemService.UpdateAsync(productItem);
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Sửa thông tin sản phẩm thành công!"));
            }
            productItemVM = _mapper.Map<ProductItemViewModel>(productItem);
            var categories = (await _categoryService.GetCategoriesWithoutSubCategories());
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productItemVM.CategoryId);
            return View(productItemVM);
        }


        [HttpGet("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(int? id)
        {
            var product = await _productItemService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (product == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            ViewData["product"] = product;
            return View(new UploadOneFile());
        }


        [HttpPost("uploadphoto"), ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int? id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = await _productItemService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (product == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction(nameof(Edit), new { id = id });
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
