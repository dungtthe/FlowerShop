using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{ 

    [Table("PaymentMethods")]
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
