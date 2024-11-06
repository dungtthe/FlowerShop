using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("Packaging")]
    public class Packaging
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string ?Description {  get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
