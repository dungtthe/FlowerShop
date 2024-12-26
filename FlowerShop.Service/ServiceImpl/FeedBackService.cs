using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
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

        public async Task<IEnumerable<FeedBack>> GetAllFeedbackAsync()
        {
            var ls = (await _feedBackRepository.GetAllAsync()).Where(p => p.IsDelete == false);
            return ls;
        }

        public async Task<IEnumerable<FeedBack>> GetFeedBackByIdAsync(int feedBackId)
        {
            return await _feedBackRepository.GetFeedBackByProductIdAsync(feedBackId);
        }
    }
    
    }
