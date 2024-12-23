﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Conversations")]
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerConversations")]
        public AppUser ?Customer { get; set; }

        public string? StaffId { get; set; }//staffid = null thì cho nó là chatbot
        [ForeignKey(nameof(StaffId))]
        [InverseProperty("StaffConversations")]
        public AppUser? Staff { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        // public bool IsChatbot { get; set; }

        public ICollection<Message>? Messages {  get; set; }


    }
}
