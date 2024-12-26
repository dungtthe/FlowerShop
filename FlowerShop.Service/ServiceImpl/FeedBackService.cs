using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUnitOfWork _unitOfWork;
        public FeedBackService(IFeedBackRepository feedBackRepository, IUnitOfWork unitOfWork)
        {
            _feedBackRepository = feedBackRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FeedBack> AddAsync(FeedBack feedBack)
        {
            var rs = await _feedBackRepository.AddAsync(feedBack);
            if (rs != null)
            {
                await _unitOfWork?.Commit();
            }
            return rs;
        }
        public async Task<IEnumerable<FeedBack>> GetAllFeedbackAsync()
        {
            var ls = (await _feedBackRepository.GetAllAsync()).Where(p => p.IsDelete == false);
            return ls;
        }

        public async Task<IEnumerable<FeedBack>> GetFeedBackByIdAsync(int feedBackId)
        {
            return await _feedBackRepository.GetFeedBackByProductIdAsync(feedBackId);
        }

        // Phương thức để lấy phản hồi dựa trên SaleInvoiceDetailId và UserId
        public async Task<FeedBack> GetFeedbackBySaleInvoiceDetailIdAsync(int saleInvoiceDetailId, string userId)
        {
            var rs = await _feedBackRepository.GetFeedbackBySaleInvoiceDetailIdAndUserIdAsync(saleInvoiceDetailId, userId);
            return rs;
           
        }
    }
    
    }
