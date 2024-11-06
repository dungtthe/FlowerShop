using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}
