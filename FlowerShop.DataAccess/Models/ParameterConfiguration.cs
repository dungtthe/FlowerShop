using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
    [Table("ParameterConfigurations")]
    public class ParameterConfiguration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AllowedFeedbackDay {  get; set; }//dùng để thiết lập số ngày được phép feedback, ví dụ = 7 thì chỉ được feedback trong vòng 7 ngày kể từ lúc giao hàng thành công
    }
}
