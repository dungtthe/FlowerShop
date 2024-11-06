using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public int AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser? AppUser { get; set; }
    }
}
