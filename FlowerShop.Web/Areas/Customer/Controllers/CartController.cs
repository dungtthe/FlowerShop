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
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IParameterService  _parameterService;
        public CartController(ICartService cartService, IAppUserService appUserService, IProductService productService,IPaymentMethodService paymentMethodService, IParameterService parameterService)
        {
            _cartService = cartService;
            _appUserService = appUserService;
            _productService = productService;
            _paymentMethodService = paymentMethodService;
            _parameterService = parameterService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> CartDetailAsync()
        {
            ViewBag.tilePage = "Thông tin giỏ hàng";
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            var cart = await _cartService.GetCartByUserIdAsync(appUser?.Id);
            if (appUser == null || cart == null || cart.CartDetails == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            cart = await _cartService.HandlerQuantityProductInCartAsync(cart);

            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
            foreach (var item in cart.CartDetails.Where(c => !c.IsDeleted))
            {
                var p = await _productService.FindOneByIdAsync(item.ProductId);
                if (p != null)
                {
                    var priceSell = p.ProductPrices.OrderBy(p => p.Priority).ToList();
                    if (priceSell != null && priceSell.Count > 0)
                    {
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



        [HttpGet("checkout")]
        public async Task<IActionResult> CheckoutAsync()
        {
            ViewBag.tilePage = "Checkout";
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            var cart = await _cartService.GetCartByUserIdAsync(appUser.Id);
            var paymentMethods = await _paymentMethodService.GetPaymentMethodsAsync();
            var paramConfig = (await _parameterService.GetData()).Where(p=>p.Id==1).FirstOrDefault();
            var sProductIds = HttpContext.Session.GetString("checkout");

            if (appUser == null || cart == null || cart.CartDetails == null || sProductIds==null || paramConfig ==null)
            {
                return RedirectToAction("NotFound", "Error");
            }


            List<int> productsId = new List<int>();
            string[]s = sProductIds.Split(' ');
            foreach(var item in s)
            {
                int productId;
                if (int.TryParse(item, out productId))
                {
                    productsId.Add(productId);
                }
            }


            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
            foreach (var item in cart.CartDetails.Where(c => !c.IsDeleted))
            {
                var p = await _productService.FindOneByIdAsync(item.ProductId);
                if (p != null && productsId.Contains(p.Id))
                {
                    var priceSell = p.ProductPrices.OrderBy(p => p.Priority).ToList();
                    if (priceSell != null && priceSell.Count > 0 )
                    {
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
            ViewBag.fullName = appUser.FullName;

            string phone = "";
            if (!string.IsNullOrEmpty(appUser.PhoneNumber))
            {
                phone = appUser.PhoneNumber;
            }
            ViewBag.phoneNumber = phone;
            ViewBag.paymentMethods = paymentMethods;
            ViewBag.shippingCostPerKilometer = paramConfig.ShippingCostPerKilometer;
            return View(cartDetails);
        }
    }
}
