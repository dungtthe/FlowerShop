using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("SaleInvoiceDetails")]
    public class SaleInvoiceDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleInvoiceId { get; set; }
        [ForeignKey(nameof(SaleInvoiceId))]
        public SaleInvoice ?SaleInvoice { get; set; } // Giả sử có class SaleInvoice

        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product ?Product { get; set; } // Giả sử có class Product

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
