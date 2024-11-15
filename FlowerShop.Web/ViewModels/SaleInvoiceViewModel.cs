using FlowerShop.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Web.ViewModels
{
	public class SaleInvoiceViewModel
	{
		public int Id { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime CreateDay { get; set; }

		[Required]
		public string? CustomerId { get; set; }

		public AppUser? Customer { get; set; }

		public int PaymentMethodId { get; set; }

		public PaymentMethod? PaymentMethod { get; set; }

		[Required]
		public byte Status { get; set; }

		[Required]
		public bool IsDelete { get; set; }
	}
}