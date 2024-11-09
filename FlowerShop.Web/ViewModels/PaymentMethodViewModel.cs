using FlowerShop.Common.MyAttribute;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class PaymentMethodViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên phương thức không được để trống")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [MinValueDecimal("fasdfasd",1000)]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public byte Status { get; set; }

        [Required(ErrorMessage = "Trạng thái xóa không được để trống")]
        public bool IsDelete { get; set; }
    }
}
