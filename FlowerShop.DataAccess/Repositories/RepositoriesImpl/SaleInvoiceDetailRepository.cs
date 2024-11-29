using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class SaleInvoiceDetailRepository : RepositoryBase<SaleInvoiceDetail>, ISaleInvoiceDetailRepository
    {
        public SaleInvoiceDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IEnumerable<int>> GetProductIdTopSellingProductsAsync(int top = 8)
        {
            var rs = await DbContext.SaleInvoiceDetails
                .Where(s => !s.IsDelete)
                .Include(p => p.Product)
                .Where(p => !p.Product.IsDelete)
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,//key ở đây là đối tượng được nhóm, trên kia chỉ có nhóm theo 1 thuộc tính là ProductId nên key ở đây là ProductId luôn
                    TotalQuantity = group.Sum(d => d.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(top)
                .Select(p => p.ProductId)
                .ToListAsync();
            return rs;
        }

    }
}
