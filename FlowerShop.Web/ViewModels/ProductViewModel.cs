using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowerShop.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Tiêu đề không được để trống")]
        [MaxLength(100)]
        [DisplayName("Tiêu đề")]
        public string? Title { get; set; }

        [MaxLength(1000)]
        [DisplayName("Mô tả")]
        public string? Description { get; set; }



        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0.")]
        [DisplayName("Giá bán")]
        public int PriceDefault { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [MaxLength(1500)]
        [DisplayName("Hình ảnh")]
        public string? Images { get; set; }

        [Required(ErrorMessage ="Cách đóng gói không được để trống")]
        [DisplayName("Cách đóng gói")]
        public int PackagingId { get; set; }

        [ForeignKey(nameof(PackagingId))]
        public Packaging? Packaging { get; set; }

        public bool IsDelete { get; set; }

        ICollection<ProductPrice>? ProductPrices { get; set; }
        ICollection<ProductProductItem>? ProductProductItems { get; set; }
        ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
