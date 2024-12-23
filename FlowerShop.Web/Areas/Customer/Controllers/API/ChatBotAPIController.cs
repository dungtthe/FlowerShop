using FlowerShop.Common.Template;
using FlowerShop.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.Controllers.API
{
    public class ChatBotAPIController
    {
       
        [ApiController]
        [Area("CUSTOMER")]
        [Route("api/chatbot")]

        public class ChatbotController : ControllerBase
        {
            [HttpPost]
            public IActionResult GetResponse([FromBody] ChatRequest request)
            {
                // Logic xử lý câu hỏi
                string userInput = request.Message;
                string botResponse;

                // Ví dụ: Tích hợp OpenAI GPT (hoặc logic trả lời đơn giản)
                if (userInput.Contains("hoa"))
                {
                    botResponse = "Bạn có thể chọn hoa hồng, hoa cúc hoặc hoa lan cho ngày 20/11.";
                }
                else
                {
                    botResponse = "Xin lỗi, tôi không hiểu câu hỏi của bạn.";
                }

                return Ok(new { response = botResponse });
            }
        }

        public class ChatRequest
        {
            public string Message { get; set; }
        }

    }
}
