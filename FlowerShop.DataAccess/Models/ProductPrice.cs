﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("ProductPrices")]
    public class ProductPrice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public  Product ?Product { get; set; }

        [Required] 
        public decimal Price { get; set; }

        [Required]
        public byte Priority { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime ?EndDate { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
