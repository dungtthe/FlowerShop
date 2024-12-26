using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
	[Table("SaleInvoices")]
	public class SaleInvoice
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime CreateDay { get; set; }

		[Required]
		public string? CustomerId { get; set; }

		[ForeignKey(nameof(CustomerId))]
		public AppUser? Customer { get; set; }

		[Required]
		public int PaymentMethodId { get; set; }

		[ForeignKey(nameof(PaymentMethodId))]
		public PaymentMethod? PaymentMethod { get; set; }

		[Required]
		public byte Status { get; set; }

		[Required]
		public bool IsDelete { get; set; }

		[MaxLength(2000)]
		public string? Note {  get; set; }
		[Required]
		public bool IsPaid {  get; set; }
        [Required]
        public int ShippingCost { get; set; }

        [Required]
        [MaxLength(255)]
        public string ? NameRecipient { get; set; }
        [Required]
		[Phone]
		[MaxLength(30)]
		public string ? PhoneNumberRecipient {  get; set; }
		[Required]
        [MaxLength(1000)]
        public string ? DeliveryAddress { get; set; }
    }
}