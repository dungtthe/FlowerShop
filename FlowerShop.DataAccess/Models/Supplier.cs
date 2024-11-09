using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string ?CompanyName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ?TaxCode { get; set; }

        [Required]
        [EmailAddress]
        public string ?Email { get; set; }

        [Required]
        [Phone]
        public string ?Phone { get; set; }

        public int Type { get; set; }

        [MaxLength(1500)]
        public string? Images { get; set; }

        public string ?Description { get; set; }

        [Required]
        [MaxLength(300)]
        public string ?Industry { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Address { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public ICollection<SupplierInvoice>? SupplierInvoices { get; set; }
    }
}
