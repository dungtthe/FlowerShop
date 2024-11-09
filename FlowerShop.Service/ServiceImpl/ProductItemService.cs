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
    public class ProductItemService : IProductItemService
    {

        private readonly IProductItemRepository _productItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductItemService(IProductItemRepository productItemRepository, ICategoryRepository categoryRepository,IUnitOfWork unitOfWork)
        {
            _productItemRepository = productItemRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<ProductItem>> GetProductsAsync()
        {
            var productItems = await _productItemRepository.GetAllWithIncludeAsync(p => p.Category);
            return productItems.ToList();
        }



        public ProductItem Update(ProductItem productItem)
        {
            var result = _productItemRepository.Update(productItem);
            if(result != null)
            {
                _unitOfWork.Commit();
            }
            return result;
        }
    }
}
