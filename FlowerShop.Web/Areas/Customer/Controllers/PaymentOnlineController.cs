using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FlowerShop.Web.Areas.Customer.Controllers
{

    [Area("CUSTOMER")]
    [Route("payment-online")]
    public class PaymentOnlineController : Controller
    {

        private readonly IAppUserService _appUserService;
        public PaymentOnlineController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            ViewBag.tilePage = "Thông tin chuyển khoản";
            ViewBag.SO_TAI_KHOAN = "0397487360";
            ViewBag.NGAN_HANG = "MBBank";
            ViewBag.SO_TIEN = "20000";
            ViewBag.NOI_DUNG = "123";
            _ = Task.Run(() => StartBackgroundTaskCheckPayment(appUser));
            return View();
        }

        private async Task StartBackgroundTaskCheckPayment(AppUser appUser)
        {
            string apiUrl = "https://my.sepay.vn/userapi/transactions/list";

            using (HttpClient client = new HttpClient())
            {
                // Thêm Authorization header 
                client.DefaultRequestHeaders.Add("Authorization", "Bearer LW0MSKXB9Y4ROFPH4XH56DYUECWKM2MLR8JVBWFIEATZCQYVTYF6PNZEZHHSULR1");

                while (true)
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            Debug.WriteLine($"API Response: {responseBody}");
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    await Task.Delay(10000);
                }
            }
        }
    }
}
