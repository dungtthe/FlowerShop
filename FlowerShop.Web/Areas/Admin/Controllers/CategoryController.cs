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

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-ly-danh-muc")]
    public class CategoryController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(FlowerShopContext context,ICategoryService categoryService,IMapper mapper)
        {
            _context = context;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: Admin/Category
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categoriesHierarchy = await _categoryService.GetAllCategoriesWithHierarchy();
            return View(categoriesHierarchy);
        }


        // GET: Admin/Category/Create
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

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentCategoryId,IsCategorySell")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {

                //Category category = new Category();
                //category.Name=categoryViewModel.Name;
                //category.ParentCategoryId=(categoryViewModel.ParentCategoryId==0)?null:categoryViewModel.ParentCategoryId;


                Category category= _mapper.Map<Category>(categoryViewModel);
                var rs= await _categoryService.AddAsync(category);
                if (rs != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Content(ConstValues.CoLoiXayRa);
                }
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

        // GET: Admin/Category/Edit/5
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var rs = await _categoryService.GetSingleByIdAsync(id ?? -1);
            if (rs == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            //var categoryVM = new CategoryViewModel
            //{
            //    Id = rs.Id,
            //    Name = rs.Name,
            //    ParentCategoryId = rs.ParentCategoryId,
            //    ParentCategory = rs.ParentCategory,
            //    IsCategorySell = rs.IsCategorySell,
            //    SubCategories = rs.SubCategories,
            //};


            var categoryVM=_mapper.Map<CategoryViewModel>(rs);  



            var categories = new List<Category>
            {
                new Category { Id = 0, Name = "Không có" }
            };
            categories.AddRange(await _categoryService.GetAllCategoriesNotWithHierarchy());

            ViewBag.Categories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name");

            ViewBag.SelectedSubCategories = categoryVM.SubCategories?.Select(c => c.Id).ToList() ?? new List<int>();

            return View(categoryVM);
        }


        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                // Nếu model không hợp lệ, trả về view cùng với dữ liệu ViewBag
                var categories = new List<Category>
                {
                    new Category { Id = 0, Name = "Không có" }
                };
                categories.AddRange(await _categoryService.GetAllCategoriesNotWithHierarchy());

                ViewBag.Categories = new SelectList(categories.Where(c => c.Id != id), "Id", "Name");

                ViewBag.SelectedSubCategories = categoryVM.SelectedSubCategories;

                return View(categoryVM);
            }

            // Xử lý cập nhật danh mục như bình thường
            var category = await _categoryService.GetSingleByIdAsync(id);
            if (category == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            category.Name = categoryVM.Name;
            category.ParentCategoryId = (categoryVM.ParentCategoryId == 0) ? null : categoryVM.ParentCategoryId;
            category.IsCategorySell = categoryVM.IsCategorySell;

            //categoryVM.Id=category.Id;
            //category=_mapper.Map<Category>(categoryVM);

            await _categoryService.Update(category, categoryVM.SelectedSubCategories);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetSingleByIdAsync(id);
            if (category == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR,"Lỗi","Không tìm thấy danh mục để xóa"));
                return RedirectToAction("Index");
            }

            // Thực hiện xóa danh mục
            PopupViewModel rsp =  await _categoryService.Delete(id);
            //TempData[nameof(PopupViewModel)] = JsonConvert.SerializeObject(rsp);
            TempData["PopupViewModel"] = JsonConvert.SerializeObject(rsp);
            return RedirectToAction("Index"); 
        }


        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
