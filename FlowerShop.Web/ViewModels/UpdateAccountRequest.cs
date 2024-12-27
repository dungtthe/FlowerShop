using Microsoft.AspNetCore.Http;

namespace FlowerShop.Web.ViewModels
{
	public class UpdateAccountRequest
	{
		public string FullName { get; set; }      // Họ và tên người dùng
		public string BirthDay { get; set; }     // Ngày sinh, định dạng yyyy-MM-dd
		public string Email { get; set; }        // Email dùng để định danh
		public string PhoneNumber { get; set; }  // Số điện thoại
		public IFormFile ProfileImage { get; set; } // File ảnh đại diện được gửi từ client
	}
}