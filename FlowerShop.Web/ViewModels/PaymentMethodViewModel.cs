using FlowerShop.Common.MyAttribute;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class PaymentMethodViewModel
    {
        public int Id { get; set; }

        
        public string? Name { get; set; }
        public string? Description { get; set; }

       
        public decimal? Price { get; set; }

        
        [Range(0, 1, ErrorMessage = "Trạng thái chỉ nhận 0 hoặc 1.")]
        public byte Status { get; set; }

        
        public bool IsDelete { get; set; }
    }
}
