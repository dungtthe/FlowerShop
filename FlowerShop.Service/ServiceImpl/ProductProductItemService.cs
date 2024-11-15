using FlowerShop.Common.Template;
using FlowerShop.Common;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerShop.DataAccess.Models;

namespace FlowerShop.Service.ServiceImpl
{
    public class ProductProductItemService : IProductProductItemService
    {

        private readonly IProductProductItemRepository _productProductItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductProductItemService(IProductProductItemRepository productProductItemRepository, IUnitOfWork unitOfWork)
        {
            this._productProductItemRepository = productProductItemRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<ProductProductItem> AddAsync(ProductProductItem productProductItem)
        {
            if (productProductItem == null)
            {
                return null;
            }

            return await _productProductItemRepository.AddAsync(productProductItem);
        }

        public async Task<bool> CheckExistPrductItem(int id)
        {
            var list=await _productProductItemRepository.GetAllAsync();
            return list.Any(x => x.ProductItemId == id);
        }

        public async Task DeleteAsync(int id)
        {
            await Task.CompletedTask;
        }
    }
}
