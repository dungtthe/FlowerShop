using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
	public class CustomerViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên khách hàng không được để trống")]
		public string? FullName { get; set; }

		public string? PhoneNumber { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? BirthDay { set; get; }

		public bool IsLock { get; set; }
		public bool IsDelete { get; set; }
	}
}