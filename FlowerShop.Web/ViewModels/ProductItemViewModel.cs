using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowerShop.Web.ViewModels
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [MaxLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [DisplayName("Giá sản phẩm")]
        public int ImportPrice { get; set; }


        [Required(ErrorMessage = "Phải thuộc 1 danh mục")]
        [DisplayName("Danh mục thuộc về")]
        public int CategoryId { get; set; }
        [DisplayName("Danh mục thuộc về")]
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }


        [MaxLength(1500)]
        [DisplayName("Hình ảnh")]
        public string? Images { get; set; }


        [DisplayName("Chi tiết")]
        public string? Description { get; set; }

        [Required]
        public bool IsDelete { get; set; }
        public ICollection<ProductProductItem>? ProductProductItems { get; set; }
    }
}
