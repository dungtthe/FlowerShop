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
using AutoMapper;
using FlowerShop.Common.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using FlowerShop.Common.Template;

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-ly-danh-muc")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categoriesHierarchy = await _categoryService.GetAllCategoriesWithHierarchy();


            //foreach(var item in categoriesHierarchy)
            //{
            //    if (item.Id != 3) continue;
            //    foreach(var sub in item.SubCategories)
            //    {
            //        Debug.WriteLine(sub.Name);
            //    }
            //}

            return View(categoriesHierarchy);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var categories = new List<Category>();
            categories.Add(new Category()
            {
                Id = 0,
                Name="Không có"
            });
            categories.AddRange(await _categoryService.GetAllCategoriesNotWithHierarchy());

            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentCategoryId,IsCategorySell")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category= _mapper.Map<Category>(categoryViewModel);
                var rs =  await _categoryService.AddAsync(category);
                if (rs.Id == ResponeMessage.SUCCESS)
                {
                    TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", rs.Message));
                }
                else
                {
                    TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Thất bại", rs.Message));
                }
                return RedirectToAction(nameof(Index));
            }

            var categories = new List<Category>();
            categories.Add(new Category()
            {
                Id = 0,
                Name = "Không có"
            });
            categories.AddRange(await _categoryService.GetAllCategoriesNotWithHierarchy());

            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name");
            return View(categoryViewModel);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var rs = await _categoryService.FindOneWithIncludeByIdAsync(id ?? -1);
            if (rs == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy danh mục!"));
                return RedirectToAction("Index");
            }
            var categoryVM=_mapper.Map<CategoryViewModel>(rs);

			//chỉ cho phép danh mục bán mới có hệ thống phân cấp
			var categories = new List<Category>
			{
				new Category { Id = 0, Name = "Không có" }
			};
			if (rs.IsCategorySell)
            {
				var temp = (await _categoryService.GetAllCategoriesNotWithHierarchy()).Where(c => c.Id != id && c.IsCategorySell);
				
				foreach (var c in temp)
				{
					var flag = await _categoryService.IsDescendantAsync(c.Id, rs.Id);
					if (!flag)
					{
						categories.Add(c);
					}
				}
			}
			ViewBag.Categories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name");
			ViewBag.SelectedSubCategories = categoryVM.SubCategories?.Select(c => c.Id).ToList() ?? new List<int>();
            return View(categoryVM);
        }

 
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                var categories = new List<Category>
                {
                    new Category { Id = 0, Name = "Không có" }
                };


				var findCat = await _categoryService.GetCategoryByIdAsync(id);
				if (findCat == null)
				{
					TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy danh mục!"));
					return RedirectToAction("Index");
				}

                //chỉ danh mục bán mới cho có hệ thống phân cấp
                if (findCat.IsCategorySell)
                {
					categories.AddRange((await _categoryService.GetAllCategoriesNotWithHierarchy()).Where(c => c.Id != id && c.IsCategorySell));
				}

				ViewBag.Categories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name");

                ViewBag.SelectedSubCategories = categoryVM.SelectedSubCategories;

                return View(categoryVM);
            }
            var rs = await _categoryService.UpdateAsync(id,categoryVM.Name,categoryVM.ParentCategoryId??-1, categoryVM.SelectedSubCategories);

            if (rs.Id == ResponeMessage.SUCCESS)
            {
				TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", rs.Message));
			}
            else
            {
				TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Thất bại", rs.Message));
			}
			return RedirectToAction("Index");
        }
    }
}
