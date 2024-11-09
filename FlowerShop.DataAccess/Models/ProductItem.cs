using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("ProductItems")]
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ImportPrice { get; set; }

        [MaxLength(1500)]
        public string ?Images { get; set; }

        public string ?Description { get; set; }

        [Required]
        public bool IsDelete { get; set; }
        public ICollection<ProductProductItem>? ProductProductItems { get; set; }

    }
}
