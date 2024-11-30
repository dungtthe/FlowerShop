using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
    public class CartDetailViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName {  get; set; }
        public string? ProductImage {  get; set; }
        public decimal? PriceSell { get; set; }
        public int ?QuantityInStock {  get; set; }
        public int QuantityInCart { get; set; }

    }
}
