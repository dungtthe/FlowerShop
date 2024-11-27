using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;  // Để sử dụng IFormFile cho việc tải ảnh lên

namespace FlowerShop.Web.ViewModels
{
	public class SupplierViewModel
	{
		#region Hiển thị lên view danh sách

		public int Id { get; set; }

		[Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
		public string? CompanyName { get; set; }

		[Required]
		[MaxLength(100)]
		public string? TaxCode { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không đúng định dạng")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Số điện thoại không được để trống")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chứa 10 số")]
		public string? Phone { get; set; }

		[Required(ErrorMessage = "Mô tả không được để trống")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Địa chỉ không được để trống")]
		[MaxLength(500)]
		public string? Address { get; set; }

		#endregion Hiển thị lên view danh sách

		public int Type { get; set; }

		[MaxLength(300)]
		public string? Industry { get; set; }

		[MaxLength(1500)]
		public string? Images { get; set; } // Trường ảnh sử dụng IFormFile để nhận file từ form

		public bool IsDelete { get; set; }
	}
}