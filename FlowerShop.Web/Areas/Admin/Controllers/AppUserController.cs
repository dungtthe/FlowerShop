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
    [Area("ADMIN")]
    [Route("admin/quan-li-nhan-vien")]
    public class AppUserController : Controller
    {
        private readonly FlowerShopContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AppUserController(FlowerShopContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        // GET: Admin/User
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            var result = await _userService.GetUsersAsync();
            if(result == null)
            {
                return NotFound();  
            }
            //var appusersVM = new List<AppUserViewModel>();
            //foreach (var user in result)
            //{
            //    var appuserVM = new AppUserViewModel()
            //    {
            //        Id = user.Id,
            //        Name = user.FullName,
            //        BirthDay = user.BirthDay,
            //        Images = user.Images,
            //        Email = user.Email,
            //    };
            //    appusersVM.Add(appuserVM);
            //}

            var appusersVM = new List<AppUserViewModel>();
            foreach (var item in result)
            {
                var appUser = _mapper.Map<AppUserViewModel>(item);
                appusersVM.Add(appUser);
            }
            return View(appusersVM);

        }


       
        // GET: Admin/User/Edit/5
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(string? id)
        {
            var appUser = await _userService.GetSingleById(id ?? "-1");
            if (appUser == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            var appUserVM = _mapper.Map<AppUserViewModel>(appUser);
            return View(appUserVM);

        }

        // POST: Admin/PaymentMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? id, [Bind("Id,FullName,BirthDay,Images,Email")] AppUserViewModel appUserVM)
        {
            var appUser = await _userService.GetSingleById(id ?? "-1");
            if (appUser == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }



            if (ModelState.IsValid)
            {
                try
                {

                    appUser.FullName = appUserVM.FullName;
                    appUser.BirthDay = appUserVM.BirthDay;
                    appUser.Images= appUserVM.Images;
                    appUser.Email = appUserVM.Email;



                    var result = await _userService.UpdateAsync(appUser);
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
            return View(appUserVM);
        }



        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetSingleById(id);
            if (user == null)
            {
                TempData["PopupViewModel"] = JsonConvert.SerializeObject(new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy nhân viên để xóa"));
            }
            PopupViewModel rsp = await _userService.Delete(user);
            TempData["PopupViewModel"] = JsonConvert.SerializeObject(rsp);
            return RedirectToAction("Index");
        }

        private bool AppUserExists(string id)
        {
            return (_context.AppUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(string id)
        {
            var user = await _userService.FindOneWithIncludeByIdAsync(id ?? "-1");
            if (user == null)
            {
                return Content(ConstValues.CoLoiXayRa);
            }
            ViewData["appuser"] = user;
            return View(new UploadOneFile());
        }


        [HttpPost("uploadphoto"), ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(string id, [Bind("FileUpload")] UploadOneFile f)
        {
            var user = await _userService.FindOneWithIncludeByIdAsync(id ?? "-1");
            if (user == null)
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
                var imgs = Utils.AddPhotoForProduct(fileName, user.Images);
                user.Images = imgs;
                await _userService.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Edit), new { id = id });
        }

    }
}
