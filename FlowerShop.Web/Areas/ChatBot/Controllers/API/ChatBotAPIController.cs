using Microsoft.AspNetCore.Mvc;
using FlowerShop.Service;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;

namespace FlowerShop.Web.Areas.ChatBot.Controllers.API
{

	[ApiController]
	[Area("CHATBOT")]
	[Route("api/chatbot")]
	public class ChatBotAPIController : ControllerBase
	{

        private readonly IProductService _productService;

        public ChatBotAPIController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> GetResponse(object? request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ToString()))
            {
                return BadRequest(new { message = "Yêu cầu không hợp lệ." });
            }

            // Logic xử lý câu hỏi
            string userInput = request.ToString();
            string botResponse;

            // Lấy danh sách tất cả sản phẩm
            var allProducts = await _productService.GetAllProductAsync();

            // Kiểm tra nếu có từ khóa "giá" trong câu hỏi
            if (userInput.Contains("giá", StringComparison.OrdinalIgnoreCase))
            {
                // Duyệt qua các sản phẩm và kiểm tra nếu tên sản phẩm xuất hiện trong userInput
                var matchedProduct = allProducts.FirstOrDefault(p =>
                    userInput.Contains(p.Title, StringComparison.OrdinalIgnoreCase));

                if (matchedProduct != null)
                {
                    // Nếu tìm thấy sản phẩm khớp, lấy giá và trả lời
                    var price = await _productService.GetPriceByNameAsync(matchedProduct.Title);
                    
                    if (price != null)
                    {
                        botResponse = $"Giá của {matchedProduct.Title} là {price} VND.";
                    }
                    else
                    {
                        botResponse = $"Xin lỗi, tôi không tìm thấy thông tin giá của {matchedProduct.Title}.";
                    }
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không tìm thấy sản phẩm nào phù hợp với yêu cầu của bạn.";
                }
            }
            else if (userInput.Contains("mô tả", StringComparison.OrdinalIgnoreCase))
            {
                // Duyệt qua các sản phẩm và kiểm tra nếu tên sản phẩm xuất hiện trong userInput
                var matchedProduct = allProducts.FirstOrDefault(p =>
                    userInput.Contains(p.Title, StringComparison.OrdinalIgnoreCase));

                if (matchedProduct != null)
                {
                    // Nếu tìm thấy sản phẩm khớp, lấy giá và trả lời
                    var description = matchedProduct.Description;

                    if (description != null)
                    {
                        botResponse = $"Mô tả sản phẩm {matchedProduct.Title} : {description} ";
                    }
                    else
                    {
                        botResponse = $"Xin lỗi, tôi không tìm thấy thông tin mô tả của {matchedProduct.Title}.";
                    }
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không tìm thấy sản phẩm nào phù hợp với yêu cầu của bạn.";
                }
            }
            else if (userInput.Contains("số lượng", StringComparison.OrdinalIgnoreCase))
            {
                // Duyệt qua các sản phẩm và kiểm tra nếu tên sản phẩm xuất hiện trong userInput
                var matchedProduct = allProducts.FirstOrDefault(p =>
                    userInput.Contains(p.Title, StringComparison.OrdinalIgnoreCase));

                if (matchedProduct != null)
                {
                    // Nếu tìm thấy sản phẩm khớp, lấy giá và trả lời
                    var quantity = matchedProduct.Quantity;

                    if (quantity != null)
                    {
                        botResponse = $"Số lượng sản phẩm {matchedProduct.Title} : {quantity} ";
                    }
                    else
                    {
                        botResponse = $"Xin lỗi, tôi không tìm thấy thông tin số lượng của {matchedProduct.Title}.";
                    }
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không tìm thấy sản phẩm nào phù hợp với yêu cầu của bạn.";
                }
            }
            else if (userInput.Contains("cách đóng gói", StringComparison.OrdinalIgnoreCase))
            {
                // Duyệt qua các sản phẩm và kiểm tra nếu tên sản phẩm xuất hiện trong userInput
                var matchedProduct = allProducts.FirstOrDefault(p =>
                    userInput.Contains(p.Title, StringComparison.OrdinalIgnoreCase));

                if (matchedProduct != null)
                {
                    // Nếu tìm thấy sản phẩm khớp, lấy giá và trả lời                  
                    var packaging = await _productService.GetPackagingByIdAsync(matchedProduct.Id);

                    if (packaging != null)
                    {
                        botResponse = $"Cách đóng gói sản phẩm {matchedProduct.Title} : {packaging.Name} ";
                    }
                    else
                    {
                        botResponse = $"Xin lỗi, tôi không tìm thấy thông tin cách đóng gói của {matchedProduct.Title}.";
                    }
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không tìm thấy sản phẩm nào phù hợp với yêu cầu của bạn.";
                }
            }
            else if (userInput.Contains("danh mục", StringComparison.OrdinalIgnoreCase))
            {
                // Duyệt qua các sản phẩm và kiểm tra nếu tên sản phẩm xuất hiện trong userInput
                var matchedProduct = allProducts.FirstOrDefault(p =>
                    userInput.Contains(p.Title, StringComparison.OrdinalIgnoreCase));

                if (matchedProduct != null)
                {
                    // Nếu tìm thấy sản phẩm khớp, lấy giá và trả lời                  
                    var categories = await _productService.GetCategoriesByProductIdAsync(matchedProduct.Id);

                    if (categories != null)
                    {
                        var categoryNames = string.Join(", ", categories.Select(c => c.Name));
                        botResponse = $"Danh mục sản phẩm {matchedProduct.Title} : {categoryNames}";
                    }
                    else
                    {
                        botResponse = $"Xin lỗi, tôi không tìm thấy thông tin danh mục của {matchedProduct.Title}.";
                    }
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không tìm thấy sản phẩm nào phù hợp với yêu cầu của bạn.";
                }
            }
            else
            {
                botResponse = "Bạn có thể cung cấp thêm thông tin được không?";
            }

            return Ok(new { message = botResponse });
        }


    }


}
