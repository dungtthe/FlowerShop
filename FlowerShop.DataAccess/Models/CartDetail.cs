using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("CartDetails")]
    public class CartDetail
    {
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart ?Cart { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ?Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
