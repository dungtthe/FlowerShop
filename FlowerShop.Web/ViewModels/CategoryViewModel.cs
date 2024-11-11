using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowerShop.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        [MaxLength(500, ErrorMessage = "Tên danh mục tối đa 500 kí tự")]
        [DisplayName("Tên danh mục")]
        public string? Name { get; set; }

        [DisplayName("Danh mục cha")]
        public int? ParentCategoryId { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { set; get; }

        [DisplayName("Loại danh mục để bán")]
        [Required(ErrorMessage = "Không được để trống trường này")]
        public bool IsCategorySell { get; set; }

        public ICollection<Category>? SubCategories { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public ICollection<ProductItem>? ProductItems { get; set; }

        public List<int> SelectedSubCategories { get; set; } = new List<int>();
    }
}
