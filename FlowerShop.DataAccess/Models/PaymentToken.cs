using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("PaymentTokens")]
    public class PaymentToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(20)]
        public string? Token {  get; set; }

        [Required]
        public DateTime TimeCreate { get; set; }//quá time là hủy đơn hàng luôn

        [Required]
        public int SaleInvoiceId { get; set; }

        [ForeignKey(nameof(SaleInvoiceId))]
        public SaleInvoice ?SaleInvoice { get; set; }
    }
}
