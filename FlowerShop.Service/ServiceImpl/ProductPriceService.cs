using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductPriceService(IProductPriceRepository productPriceRepository, IUnitOfWork unitOfWork)
        {
            _productPriceRepository = productPriceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProductPrice(int productId, int price)
        {
            await Task.CompletedTask;
            return false;
        }
    }
}
