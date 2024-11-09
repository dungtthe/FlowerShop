using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName {  get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
