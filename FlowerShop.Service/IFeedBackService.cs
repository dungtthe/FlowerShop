using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IFeedBackService
    {
        Task<IEnumerable<FeedBack>> GetAllFeedbackAsync();
        Task<IEnumerable<FeedBack>> GetFeedBackByIdAsync(int productId);
        Task<FeedBack> AddAsync(FeedBack feedBack);
        Task<FeedBack> GetFeedbackBySaleInvoiceDetailIdAsync(int saleInvoiceDetailId, string userId);
    }
}
