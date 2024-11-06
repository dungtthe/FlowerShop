using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("SupplierInvoiceDetails")]
    public class SupplierInvoiceDetail
    {
        [Key, Column(Order = 0)]
        [Required]
        public int SupplierInvoiceId { get; set; }
        [ForeignKey(nameof(SupplierInvoiceId))]
        public  SupplierInvoice? SupplierInvoice { get; set; } 


        [Key, Column(Order = 1)]
        [Required]
        public int ProductItemId { get; set; }
        [ForeignKey(nameof(ProductItemId))]
        public  ProductItem ?ProductItem { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }

       
    }
}
