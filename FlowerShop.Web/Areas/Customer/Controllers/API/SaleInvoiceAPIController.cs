using FlowerShop.Common.Template;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{
    [Area("CUSTOMER")]
    [Route("/api/hoa-don")]
    [ApiController]
    [Authorize]
    public class SaleInvoiceAPIController : ControllerBase
    {

        private readonly ISaleInvoiceService _saleInvoiceService;
        private readonly IAppUserService _appUserService;
        public SaleInvoiceAPIController(ISaleInvoiceService saleInvoiceService, IAppUserService appUserService)
        {
            _saleInvoiceService = saleInvoiceService;
            _appUserService = appUserService;
        }


        [HttpPost("confirm-checkout")]
        public async Task<IActionResult> ConfirmCheckoutAsync(ConfirmCheckoutViewmodel? data)
        {
            try
            {

                if (data == null)
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ" });
                }

                var user = await _appUserService.GetAppUserByContextAsync(HttpContext);
                var rs = await _saleInvoiceService.ConfirmCheckoutAsync(user, data.FullName, data.PhoneNumber, data.Address, data.Note, data.Fee ?? -1, data.SelectedPaymentMethodId ?? -1, HttpContext.Session.GetString("checkout"));
                if (rs.Id == ResponeMessage.SUCCESS)
                {
                    return Ok(new { message = rs.Message });
                }
                return BadRequest(new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return BadRequest("Có lỗi xảy ra");
            }
        }
    }
}




