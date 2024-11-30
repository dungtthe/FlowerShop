using FlowerShop.Common.Template;
using FlowerShop.Service;
using FlowerShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace FlowerShop.Web.Areas.Customer.Controllers
{

    [Area("CUSTOMER")]
    [Route("cart")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAppUserService _appUserService;
        private readonly IProductService _productService;
        public CartController(ICartService cartService, IAppUserService appUserService, IProductService productService)
        {
            _cartService = cartService;
            _appUserService = appUserService;
            _productService = productService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> CartDetailAsync()
        {
            ViewBag.tilePage = "Thông tin giỏ hàng";
            var appUser = await _appUserService.GetAppUser(HttpContext);
            var cart = await _cartService.GetCartByUserIdAsync(appUser.Id);

            if (appUser == null || cart == null || cart.CartDetails == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
            foreach (var item in cart.CartDetails.Where(c => !c.IsDeleted))
            {
                var p = await _productService.FindOneByIdAsync(item.ProductId);
                if (p != null)
                {
                    var priceSell = p.ProductPrices.OrderBy(p => p.Priority).ToList();
                    if (priceSell != null && priceSell.Count > 0)
                    {

                        if (item.Quantity > p.Quantity)
                        {
                            item.Quantity = p.Quantity;
                        }

                        string[] listImgInProduct = p.Images != null
                         ? JsonConvert.DeserializeObject<string[]>(p.Images)
                         : new string[] { "no_img.png" };
                        var firstImg = listImgInProduct[0];
                        cartDetails.Add(new CartDetailViewModel()
                        {
                            ProductId = p.Id,
                            ProductName = p.Title,
                            PriceSell = priceSell[0].Price,
                            QuantityInStock = p.Quantity,
                            QuantityInCart = item.Quantity,
                            ProductImage = firstImg

                        });
                    }

                }
            }
            return View(cartDetails);
        }
    }
}
