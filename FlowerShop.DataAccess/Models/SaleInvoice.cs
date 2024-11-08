using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("SaleInvoices")]
    public class SaleInvoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreateDay { get; set; }

        [Required]
        public string? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public AppUser? Customer { get; set; }

        [Required]
        public int PaymentMethodId {  get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        public PaymentMethod? PaymentMethod { get; set; }

        [Required]
        public byte Status {  get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
