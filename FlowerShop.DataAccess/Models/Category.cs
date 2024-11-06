using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string ?Name { get; set; }

        public int? ParentCategoryId { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public Category ?ParentCategory { set; get; }

        public  ICollection<Category> ?SubCategories { get; set; } 
        public  ICollection<ProductCategory> ? ProductCategories { get; set; } 
    }
}
