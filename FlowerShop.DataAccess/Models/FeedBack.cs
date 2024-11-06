using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("FeedBacks")]
    public class FeedBack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleInvoiceDetailId { get; set; }
        [ForeignKey(nameof(SaleInvoiceDetailId))]
        public SaleInvoiceDetail ?SaleInvoiceDetail { get; set; } 

        [Required]
        [MaxLength(500)]
        public string ?Content { get; set; }

        [Required]
        public DateTime SendingTime { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public ICollection<FeedBackResponse> ?FeedBackResponses { get; set; }
    }
}
