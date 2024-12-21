using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
	public class CustomerViewModel
	{
		public string Id { get; set; }

		[Required(ErrorMessage = "Tên khách hàng không được để trống")]
		public string? FullName { get; set; }

		[Required(ErrorMessage = "Số điện thoại không được để trống")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chứa đúng 10 chữ số")]
		public string? PhoneNumber { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		[Required(ErrorMessage = "Ngày sinh không được để trống")]
		public DateTime? BirthDay { set; get; }

		[MaxLength(1500)]
		public string? Images { get; set; }

		public bool IsLock { get; set; }
		public bool IsDelete { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không đúng định dạng")]
		public string? Email { get; set; }
	}
}