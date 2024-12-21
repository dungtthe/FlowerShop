namespace FlowerShop.Web.ViewModels
{
	public class RequestDeletedOrder
	{
		public int? Id { get; set; }
		public string? reason { get; set; }  // Đảm bảo trường này có tên chính xác và kiểu dữ liệu phù hợp
	}
}