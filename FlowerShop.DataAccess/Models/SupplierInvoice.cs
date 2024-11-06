using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("SupplierInvoices")]
    public class SupplierInvoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreateDay {  get; set; }

        [Required]
        public int SupplierId {  get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier ?Supplier { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public ICollection<SupplierInvoiceDetail> ?SupplierInvoiceDetails {  get; set; }
    }
}
