using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FlowerShop.Web.ViewModels
{
	public class PackagingViewModel
	{
		public int ?Id { get; set; }

		[Required]
		[DisplayName("Tên cách đóng gói")]
		public string ?Name { get; set; }

		[DisplayName("Mô tả")]
		public string ?Description { get; set; }

		[DisplayName("IsDelete")]
		public bool IsDelete { get; set; }
	}
}
