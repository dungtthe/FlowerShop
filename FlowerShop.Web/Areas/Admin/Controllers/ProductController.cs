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
using FlowerShop.Web.Mappings;
using FlowerShop.Common.Helpers;
using FlowerShop.Common.MyConst;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-li-sp")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductItemService _productItemService;
        private readonly ICategoryService _categoryService;
        private readonly IPackagingService _packagingService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, IProductItemService productItemService, ICategoryService categoryService, IPackagingService packagingService)
        {
            _productService = productService;
            _mapper = mapper;
            _productItemService = productItemService;
            _categoryService = categoryService;
            _packagingService = packagingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsForIndexInAdminAsync();
            if (products == null)
            {
                return NotFound();
            }

            var productsVM = new List<ProductViewModel>();
            foreach (var item in products)
            {
                var productVM = _mapper.Map<ProductViewModel>(item);
                productsVM.Add(productVM);
            }
            return View(productsVM);
        }


        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var productsItem = await _productItemService.GetProductsItemAsync();
            var categories = (await _categoryService.GetCategoriesWithoutSubCategories()).Where(c => c.IsCategorySell);
            var packagings = await _packagingService.GetAllPackagingAsync();
            if (productsItem == null || categories == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Có lỗi xảy ra!"));
                return RedirectToAction("Index");
            }
            ViewBag.ProductsItem = productsItem;
            ViewBag.categories = categories;
            ViewBag.packagings = new SelectList(packagings, "Id", "Name");
            return View();
        }

    
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _productService.FindOneByIdAsync(id ?? -1);
            var productsItem = await _productItemService.GetProductsItemAsync();
            var categories = (await _categoryService.GetCategoriesWithoutSubCategories()).Where(c => c.IsCategorySell);
            var packagings = await _packagingService.GetAllPackagingAsync();

            if (product == null )
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction("Index");
            }


            if (categories == null || productsItem ==null || categories==null || packagings==null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Có lỗi xảy ra!"));
                return RedirectToAction("Index");
            }

            EditProductViewModel editProductViewModel = ProductMapping.MapToEditProductViewModel(product, productsItem, categories);


            ViewBag.packagings = new SelectList(packagings, "Id", "Name");





            return View(editProductViewModel);

        }



        [HttpGet("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(int? id)
        {
            var product = await _productService.FindOneByIdAsync(id ?? -1);
            if (product == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction("Edit", new { id });
            }
            ViewData["productName"] = product.Title;
            ViewData["idProduct"] = product.Id;
            return View(new UploadMultipleFiles());
        }





        [HttpPost("uploadphoto"), ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int? id, [Bind("Files")] UploadMultipleFiles model)
        {
            var product = await _productService.FindOneByIdAsync(id ?? -1);
            if (product == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy sản phẩm!"));
                return RedirectToAction("Edit", new { id });
            }


            if (model.Files != null && model.Files.Any())
            {
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

                foreach (var file in model.Files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                                      + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(wwwRootPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var imgs = Utils.AddPhotoForProduct(fileName, product.Images);
                        product.Images = imgs;
                    }
                }

                await _productService.UpdateImageAsync(product);
            }
            return RedirectToAction(nameof(Edit), new { id = id });
        }


    }
}
