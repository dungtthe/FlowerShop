using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class AppUserViewModel
    {
        public string ?Id {  get; set; }


        [Required]
        [DisplayName("Họ tên")]
        public string ?FullName { get;set; }

        [DisplayName("Ngày sinh")]
        public DateTime? BirthDay { get; set; }


        [DisplayName("Hình ảnh")]
        public string ?Images {  get; set; }

        [DisplayName("Username")]
        public string ?Username { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("IsLock")]
        public bool IsLock { get; set; }

    }
}
