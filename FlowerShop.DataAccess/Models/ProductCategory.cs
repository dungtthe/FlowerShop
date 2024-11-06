using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key, Column(Order = 0)]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category ?Category { get; set; }


        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
