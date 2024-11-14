using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ?Title { get; set; }

        [MaxLength(1000)]
        public string ?Description { get; set; }


        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity {  get; set; }


        [MaxLength(1500)]
        public string ?Images { get; set; }

        [Required]
        public int PackagingId { get; set; }

        [ForeignKey(nameof(PackagingId))]
        public Packaging ?Packaging { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        ICollection<ProductPrice> ?ProductPrices { get; set; }
        ICollection<ProductProductItem> ? ProductProductItems { get; set; }
        ICollection<ProductCategory> ? ProductCategories { get; set; }
    
    }
}
