using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public Conversation ?Conversation { get; set; }

        [Required]
        public DateTime SendingTime { get; set; }

        [Required]
        public bool IsCustomer { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
