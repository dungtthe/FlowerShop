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
    public class FeedBackRepository : RepositoryBase<FeedBack>, IFeedBackRepository
    {
        private readonly FlowerShopContext _context;
        public FeedBackRepository(FlowerShopContext context, IDbFactory dbFactory) : base(dbFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedBack>> GetFeedBackByProductIdAsync(int productId)
        {
            Console.WriteLine($"Fetching feedback for ProductId: {productId}...");

            // Kiểm tra số lượng bản ghi trong bảng FeedBacks
            var totalRecords = await _context.FeedBacks.CountAsync();
            Console.WriteLine($"Total records in FeedBacks table: {totalRecords}");

            // Truy vấn bảng SaleInvoiceDetails để lấy các SaleInvoiceDetailId tương ứng với ProductId
            var saleInvoiceDetails = await _context.SaleInvoiceDetails
                .Where(sid => sid.ProductId == productId && !sid.IsDelete)
                .Select(sid => sid.Id)
                .ToListAsync();

            Console.WriteLine($"Found {saleInvoiceDetails.Count} SaleInvoiceDetails for ProductId: {productId}");

            // Sử dụng SaleInvoiceDetailId để lấy các Feedback
            var feedbacks = await _context.FeedBacks
                .Where(fb => saleInvoiceDetails.Contains(fb.SaleInvoiceDetailId) && !fb.IsDelete)
                .Include(fb => fb.SaleInvoiceDetail) // Bao gồm mối quan hệ với SaleInvoiceDetail
                .ToListAsync();

            Console.WriteLine($"Found {feedbacks.Count} feedbacks for ProductId: {productId}");

            // Kiểm tra kết quả
            if (feedbacks.Any())
            {
                foreach (var feedback in feedbacks)
                {
                    Console.WriteLine($"Found feedback with Id: {feedback.Id}, Content: {feedback.Content}");
                }
            }
            else
            {
                Console.WriteLine($"No feedback found for ProductId: {productId}");
            }

            return feedbacks;
        }



    }
}
