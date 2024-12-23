using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.ChatBot.Controllers.API
{

	[ApiController]
	[Area("CHATBOT")]
	[Route("api/chatbot")]
	public class ChatBotAPIController : ControllerBase
	{
		[HttpPost("chat")]
		public IActionResult GetResponse(object? request)
		{
			// Logic xử lý câu hỏi
			string userInput = request.ToString();
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

			return Ok(new { message = botResponse });
		}
	}

	public class ChatRequest
	{
		public string? Message { get; set; }
	}
}
