using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.Service.ServiceImpl;
using System.Text.Json;
using FlowerShop.Common.ViewModels;

namespace FlowerShop.Web.Areas.Customer.Controllers
{
    [Area("CUSTOMER")]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPackagingService _packagingService;
        private readonly IFeedBackService _feedBackService; 
        private readonly IAppUserService _appUserService; 
        private readonly ISaleInvoiceService _saleInvoiceService; 
        private readonly ISaleInvoiceDetailService _saleInvoiceDetailService; 
        private readonly FlowerShopContext _context;

        public ProductController(FlowerShopContext context, ICategoryService categoryService, IProductService productService, IPackagingService packagingService, IFeedBackService feedBackService, IAppUserService appUserService, ISaleInvoiceService saleInvoiceService, ISaleInvoiceDetailService saleInvoiceDetailService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _packagingService = packagingService;
            _context = context;
            _feedBackService = feedBackService;
            _appUserService = appUserService;
            _saleInvoiceService = saleInvoiceService;
            _saleInvoiceDetailService = saleInvoiceDetailService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> DetailAsync(int ?id)
        {
            try
            {
                // Lấy thông tin sản phẩm qua Service
                var product = await _productService.GetProductByIdAsync(id??-1);
                var topSellingProducts = await _productService.GetTopSellingProductsAsync();

                if (product == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                // Lấy danh mục liên quan (Chỉ lấy 1 danh mục)
                var categories = await _productService.GetCategoriesByProductIdAsync(id ?? -1);
                var category = categories.FirstOrDefault(); // Lấy danh mục đầu tiên

                // Lấy danh sách sản phẩm cùng danh mục
                var relatedProducts = new List<Product>();
                if (category != null)
                {
                    relatedProducts = (await _productService.GetProductsByCategoryAsync(category.Id)).ToList();
                }


                // Lấy thông tin đóng gói qua Service
                var packaging = await _productService.GetPackagingByIdAsync(product.PackagingId);
                
                var reviews = await _feedBackService.GetFeedBackByIdAsync(product.Id);

                if (reviews != null)
                {
                    foreach (var feedback in reviews)
                    {
                        // Lấy thông tin SaleInvoiceDetail tương ứng từ Feedback
                        var saleInvoiceDetail = await _saleInvoiceDetailService.GetSaleInvoiceDetailByIdAsync(feedback.SaleInvoiceDetailId);

                        if (saleInvoiceDetail != null)
                        {
                            // Lấy thông tin SaleInvoice từ SaleInvoiceDetail
                            var saleInvoice = await _saleInvoiceService.GetSaleInvoiceByIdAsync(saleInvoiceDetail.SaleInvoiceId);

                            if (saleInvoice != null)
                            {
                                // Lấy thông tin Customer từ SaleInvoice
                                var customer = await _appUserService.GetUserByIdAsync(saleInvoice.CustomerId);
                                if (customer != null)
                                {
                                    // Truyền tên khách hàng vào ViewBag
                                    ViewBag.CustomerName = customer.FullName;
                                }
                            }
                        }
                    }
                }


                // Truyền dữ liệu qua ViewBag
                ViewBag.Category = category;
                ViewBag.Packaging = packaging;
                ViewBag.Quantity = product.Quantity; // Thêm số lượng tồn vào ViewBag
                ViewBag.topSellingProducts = topSellingProducts;
                ViewBag.RelatedProducts = relatedProducts; // Truyền sản phẩm cùng danh mục vào ViewBag
                ViewBag.Reviews = reviews;

                return View("ProductDetail", product);
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost("postFeedback")]
        public async Task<IActionResult> PostFeedback([FromBody] JsonElement feedbackModel) // Sử dụng JsonElement
        {
            // Kiểm tra sự hợp lệ của dữ liệu
            if (feedbackModel.ValueKind == JsonValueKind.Undefined ||
            !feedbackModel.TryGetProperty("content", out JsonElement contentElement) ||
            string.IsNullOrWhiteSpace(contentElement.GetString()) ||
            !feedbackModel.TryGetProperty("productId", out JsonElement productIdElement))
            {
                return Ok(new PopupViewModel
                {
                    Type = PopupViewModel.ERROR,
                    Message = "Dữ liệu phản hồi không hợp lệ."
                });
            }
            // Lấy thông tin người dùng từ HttpContext
            var appUser = await _appUserService.GetAppUserByContextAsync(HttpContext);
            if (appUser == null)
            {
                return Unauthorized("Bạn cần đăng nhập để gửi phản hồi.");
            }

            // Kiểm tra xem người dùng đã mua sản phẩm chưa
            var saleInvoiceDetails = await _saleInvoiceDetailService.GetSaleInvoiceDetailsByUserIdAsync(appUser.Id);

            // Lấy ProductId từ feedbackModel
            int productIdInt = productIdElement.GetInt32(); // Lấy giá trị ProductId

            // Lấy thông tin hóa đơn tương ứng với ProductId
            var saleInvoice = await _saleInvoiceService.GetSaleInvoiceByProductIdAsync(productIdInt);

            // Kiểm tra xem hóa đơn có tồn tại và có thuộc về người dùng không
            if (saleInvoice == null || saleInvoice.CustomerId != appUser.Id)
            {
                return Ok(new PopupViewModel
                {
                    Type = PopupViewModel.ERROR,
                    Title = "Thông báo",
                    Message = "Bạn phải mua sản phẩm này trước khi có thể bình luận."
                });
            }

            // Lấy SaleInvoiceDetail đầu tiên tương ứng với sản phẩm
            var saleInvoiceDetail = saleInvoiceDetails
                .FirstOrDefault(detail => detail.ProductId == productIdInt);

            // Kiểm tra xem có tìm thấy SaleInvoiceDetail không
            if (saleInvoiceDetail == null)
            {
                return Ok(new PopupViewModel
                {
                    Type = PopupViewModel.ERROR,
                    Title = "Thông báo",
                    Message = "Không tìm thấy chi tiết hóa đơn cho sản phẩm này."
                });
            }

            // Kiểm tra xem người dùng đã gửi phản hồi cho SaleInvoiceDetail này chưa
            var existingFeedback = await _feedBackService.GetFeedbackBySaleInvoiceDetailIdAsync(saleInvoiceDetail.Id, appUser.Id);
            if (existingFeedback != null)
            {
                return Ok(new PopupViewModel
                {
                    Type = 2,
                    Title = "Thông báo",
                    Message = "Bạn đã gửi phản hồi cho sản phẩm này rồi."
                });
            }

            // Tạo một thực thể feedback mới và lưu vào cơ sở dữ liệu
            var feedback = new FeedBack
            {
                Content = contentElement.GetString(), // Lấy giá trị nội dung
                SendingTime = DateTime.UtcNow,
                SaleInvoiceDetailId = saleInvoiceDetail.Id // Gán ID nếu cần
            };

            await _feedBackService.AddAsync(feedback);
            return Ok(new PopupViewModel
            {
                Type = PopupViewModel.SUCCESS,
                Message = "Phản hồi đã được gửi thành công!"
            });
        }
    }
}
