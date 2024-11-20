using AutoMapper;
using FlowerShop.Common.Helpers;
using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace FlowerShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/quan-li-cach-dong-goi")]
	public class PackagingController : Controller
	{
		private readonly FlowerShopContext _context;
		private readonly IPackagingService _packagingService;
		private readonly IMapper _mapper;
		public PackagingController(FlowerShopContext context, IPackagingService packagingService, IMapper mapper)
		{
			_context = context;
			_packagingService = packagingService;
			_mapper = mapper;
		}



		// GET: Admin/Packaging
		[HttpGet("")]
		public async Task<IActionResult> Index()
		{

			var result = await _packagingService.GetAllPackagingAsync();
			if (result == null)
			{
				return NotFound();
			}

			var packagingVM = new List<PackagingViewModel>();
			foreach (var item in result)
			{
				var packaging = _mapper.Map<PackagingViewModel>(item);
				packagingVM.Add(packaging);
			}
			return View(packagingVM);

		}


        // GET: Admin/Packaging/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Packaging/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDelete")] PackagingViewModel packagingVM)
        {
            // Kiểm tra trùng lặp tên cách đóng gói
            var allPackages = await _packagingService.GetAllPackagingAsync();
            var isNameDuplicate = allPackages.Any(item => item.Name.Equals(packagingVM.Name, StringComparison.OrdinalIgnoreCase));

            if (isNameDuplicate)
            {
                ModelState.AddModelError("Name", "Tên cách đóng gói đã tồn tại. Vui lòng nhập tên khác.");
            }

            if (ModelState.IsValid)
            {
                // Chuyển đổi ViewModel thành Entity
                var packaging = _mapper.Map<Packaging>(packagingVM);

                // Thêm mới cách đóng gói
                var result = await _packagingService.AddAsync(packaging);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Content(ConstValues.CoLoiXayRa);
                }
            }

            // Nếu có lỗi, trả về lại trang với dữ liệu cũ
            return View(packagingVM);
        }



        // GET: Admin/Packaging/Edit
        [HttpGet("edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			var packaging = await _packagingService.FindOneById(id ?? -1);
			if (packaging == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}
			var packagingVM = _mapper.Map<PackagingViewModel>(packaging);
			return View(packagingVM);

		}

		// POST: Admin/Packaging/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,IsDelete")] PackagingViewModel packagingVM)
		{
			var packaging = await _packagingService.FindOneById(id ?? -1);
			if (packaging == null)
			{
				return Content(ConstValues.CoLoiXayRa);
			}

            // Kiểm tra trùng lặp tên
            var allPackages = await _packagingService.GetAllPackagingAsync();
            var isNameDuplicate = allPackages.Any(item => item.Name.Equals(packagingVM.Name, StringComparison.OrdinalIgnoreCase) && item.Id != packaging.Id);

            if (isNameDuplicate)
            {
                ModelState.AddModelError("Name", "Tên cách đóng gói đã tồn tại. Vui lòng nhập tên khác.");
            }



            if (ModelState.IsValid)
			{
				try
				{

					packaging.Name = packagingVM.Name;
					packaging.Description = packagingVM.Description;
					packaging.IsDelete = packagingVM.IsDelete;


					var result = await _packagingService.UpdateAsync(packaging);
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
			return View(packagingVM);
		}




	}
}
