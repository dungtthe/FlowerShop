namespace FlowerShop.Web.ViewModels
{
    public class ConfirmCheckoutViewmodel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? Fee { get; set; }
        public int? SelectedPaymentMethodId { get; set; }
        public string? Note { get; set; }
    }
}
