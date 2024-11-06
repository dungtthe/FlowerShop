using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("FeedBackResponses")]
    public class FeedBackResponse
    {
        public int FeedBackId { get; set; }
        [ForeignKey(nameof(FeedBackId))]
        public FeedBack ?FeedBack { get; set; }

        [Required]
        [MaxLength(500)]
        public string ?Content { get; set; }

        [Required]
        public DateTime SendingTime { get; set; }

        [Required]
        public bool IsCustomer { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
