using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("ProductProductItems")]
    public class ProductProductItem
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product ?Product { get; set; }


        [Key, Column(Order = 1)]
        public int ProductItemId { get; set; }
        [ForeignKey(nameof(ProductItemId))]
        public ProductItem ?ProductItem { get; set; }
    }
}
