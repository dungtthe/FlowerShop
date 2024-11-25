using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class UploadMultipleFiles
    {
        [Required(ErrorMessage = "Phải chọn ít nhất một file upload")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "Chỉ được upload file có định dạng png, jpg, jpeg, gif")]
        [Display(Name = "Chọn các file upload")]
        public IEnumerable<IFormFile>? Files { get; set; }
    }
}
