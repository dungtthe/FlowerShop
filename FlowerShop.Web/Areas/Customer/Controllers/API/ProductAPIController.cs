using FlowerShop.DataAccess.Models;
using FlowerShop.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.WebSockets;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{



    [Area("CUSTOMER")]
    [Route("/api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> DetailProductAsync(int? id)
        {
            return Ok(new { redirect = $"/product/detail?id={id ?? -1}" });
        }


        [HttpGet("newproducts")]
        public async Task<IActionResult> GetNewsProducts(string? pageIndexCur)
        {

            int pageNext;
            if (!int.TryParse(pageIndexCur, out pageNext))
            {
                return BadRequest();
            }
            pageNext++;
            var result = await _productService.GetNewProductsAsync(pageNext);
            if (result.products == null || !result.products.Any())
            {
                return BadRequest();
            }
            JObject obj = new JObject();
            obj.Add("pageIndexCur", pageNext);
            obj.Add("remaining", result.remaining);

            var products = new List<JObject>();
            foreach (var item in result.products)
            {
                string[] listImgInProduct = item.Images != null
                ? JsonConvert.DeserializeObject<string[]>(item.Images)
                : new string[] { "no_img.png" };
                var firstImg = listImgInProduct[0];
                var productPrices = item.ProductPrices.OrderBy(p => p.Priority).ToList();
                var priceDefault = (productPrices.Where(p => p.StartDate == null && p.EndDate == null).First()).Price;
                var priceSale = productPrices[0].Price;

                var p = new JObject()
                {
                    {"id",item.Id },
                    {"image",firstImg },
                    {"priceDefault",priceDefault },
                    {"priceSale",priceSale },
                    {"title",item.Title }
                };
                products.Add(p);
            }
            obj.Add("products", JArray.FromObject(products));
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Content(jsonString, "application/json");

        }


        [HttpGet("giftproducts")]
        public async Task<IActionResult> GetGiftProductsProducts(string? pageIndexCur)
        {

            int pageNext;
            if (!int.TryParse(pageIndexCur, out pageNext))
            {
                return BadRequest();
            }
            pageNext++;
            var result = await _productService.GetGiftCategoryProductsAsync(pageNext);
            if (result.products == null || !result.products.Any())
            {
                return BadRequest();
            }
            JObject obj = new JObject();
            obj.Add("pageIndexCur", pageNext);
            obj.Add("remaining", result.remaining);

            var products = new List<JObject>();
            foreach (var item in result.products)
            {
                string[] listImgInProduct = item.Images != null
                ? JsonConvert.DeserializeObject<string[]>(item.Images)
                : new string[] { "no_img.png" };
                var firstImg = listImgInProduct[0];
                var productPrices = item.ProductPrices.OrderBy(p => p.Priority).ToList();
                var priceDefault = (productPrices.Where(p => p.StartDate == null && p.EndDate == null).First()).Price;
                var priceSale = productPrices[0].Price;

                var p = new JObject()
                {
                    {"id",item.Id },
                    {"image",firstImg },
                    {"priceDefault",priceDefault },
                    {"priceSale",priceSale },
                    {"title",item.Title }
                };
                products.Add(p);
            }
            obj.Add("products", JArray.FromObject(products));
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Content(jsonString, "application/json");

        }
    }
}
