using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("SupplierInvoiceTokens")]
    public class SupplierInvoiceToken
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? TokenEmail {  get; set; }

        [Required]
        [MaxLength(1006)]
        public string ? TokenAccept {  get; set; }//== TokenEmail+"accept"

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int SupplierInvoiceId { get; set; }
        [ForeignKey(nameof(SupplierInvoiceId))]
        public SupplierInvoice? SupplierInvoice { get; set; }

    }
}
