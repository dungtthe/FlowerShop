using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface ISupplierInvoice
	{
		Task<ICollection<SupplierInvoice>> GetSuppliersInvoiceAsync();

		Task<SupplierInvoice> GetSingleById(int id);
	}
}