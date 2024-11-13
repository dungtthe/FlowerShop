using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
	public class SupplierViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
		public string? CompanyName { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không đúng định dạng")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Số điện thoại không được để trống")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chứa đúng 10 chữ số")]
		public string? Phone { get; set; }

		[Required(ErrorMessage = "Mô tả không được để trống")]
		public string? Description { get; set; }
	}
}