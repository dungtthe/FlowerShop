using FlowerShop.Common.Template;
using FlowerShop.Common;
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

        public async Task DeleteAsync(int id)
        {
            var productItem = await _productItemRepository.GetSingleByIdAsync(id);
            if (productItem == null)
            {
                return;
            }
            productItem.IsDelete = true;
            await _unitOfWork.Commit();
        }



        public async Task<ICollection<ProductItem>> GetProductsItemAsync()
        {
            var productItems = await _productItemRepository.GetAllWithIncludeAsync(p => p.Category);
            return productItems.ToList();
        }

        public async Task<ProductItem> GetSingleById(int id)
        {
            if (id == -1)
            {
                return null;
            }
            var productItem = await _productItemRepository.SingleOrDefaultWithIncludeAsync(p=>p.Id==id , p=>p.Category);
            return productItem;
        }

        public async Task<ProductItem> UpdateAsync(ProductItem productItem)
        {
            var result = _productItemRepository.Update(productItem);
            if(result != null)
            {
                await _unitOfWork.Commit();
            }
            return result;
        }
    }
}
