﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{

    [Table("ProductCategories")]
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category ?Category { get; set; }


        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
