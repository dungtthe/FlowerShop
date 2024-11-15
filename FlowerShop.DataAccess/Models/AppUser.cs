using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Models
{
	[Table("AppUsers")]
	public class AppUser : IdentityUser
	{
		[Required]
		[MaxLength(255)]
		public string? FullName { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? BirthDay { set; get; }

		[MaxLength(1500)]
		public string? Images { get; set; }

		[Required]
		public bool IsLock { get; set; }

		[Required]
		public bool IsDelete { get; set; }

		[Required]
		public int CartId { get; set; }

		[ForeignKey(nameof(CartId))]
		public Cart? Cart { get; set; }

		public ICollection<Address>? Addresses { get; set; }
		public ICollection<SaleInvoice>? SaleInvoices { get; set; }

		[InverseProperty("Customer")]
		public ICollection<Conversation>? CustomerConversations { get; set; }

		[InverseProperty("Staff")]
		public ICollection<Conversation>? StaffConversations { get; set; }
	}
}