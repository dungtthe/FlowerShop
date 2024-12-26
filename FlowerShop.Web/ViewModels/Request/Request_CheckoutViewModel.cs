using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels.Request
{
    public class Request_CheckoutViewModel
    {
        public int[] ?productsId { get; set; }
        public int[] ?quantities { get; set; }
    }
}





