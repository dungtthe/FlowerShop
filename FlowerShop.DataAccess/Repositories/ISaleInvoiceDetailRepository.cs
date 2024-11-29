using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories
{
    public interface ISaleInvoiceDetailRepository : IRepository<SaleInvoiceDetail>
    {
        Task<IEnumerable<int>> GetProductIdTopSellingProductsAsync(int top = 8);
    }
}
