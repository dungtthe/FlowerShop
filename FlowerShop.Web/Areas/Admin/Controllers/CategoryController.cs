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

namespace FlowerShop.Web.Areas.Admin.Controllers
{
    [Area("ADMIN")]
    [Route("admin/quan-ly-danh-muc")]
    public class CategoryController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly ICategoryService _categoryService;

        public CategoryController(FlowerShopContext context,ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        // GET: Admin/Category
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //var flowerShopContext = _context.Categories.Include(c => c.ParentCategory);
            //return View(await flowerShopContext.ToListAsync());

            var categoriesHierarchy = await _categoryService.GetAllCategoriesWithHierarchy();
            return View(categoriesHierarchy);
        }

        // GET: Admin/Category/Details/5
        [HttpGet("detail")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
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

                Category category = new Category();
                category.Name=categoryViewModel.Name;
                category.ParentCategoryId=(categoryViewModel.ParentCategoryId==0)?null:categoryViewModel.ParentCategoryId;
               
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

            var categoryVM = new CategoryViewModel
            {
                Id = rs.Id,
                Name = rs.Name,
                ParentCategoryId = rs.ParentCategoryId,
                ParentCategory = rs.ParentCategory,
                IsCategorySell = rs.IsCategorySell,
                SubCategories = rs.SubCategories,
            };

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
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
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

                ViewBag.SelectedSubCategories = model.SelectedSubCategories;

                return View(model);
            }

            // Xử lý cập nhật danh mục như bình thường
            var category = await _categoryService.GetSingleByIdAsync(id);
            if (category == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            category.Name = model.Name;
            category.ParentCategoryId = (model.ParentCategoryId==0)?null:model.ParentCategoryId;
            category.IsCategorySell = model.IsCategorySell;
             await _categoryService.Update(category, model.SelectedSubCategories);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetSingleByIdAsync(id);
            if (category == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }

            // Thực hiện xóa danh mục
           var rsp=  await _categoryService.Delete(id);
            if (rsp.Id == 0)
            {
                return Content(rsp.Message);
            }
            return RedirectToAction("Index"); 
        }


        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
