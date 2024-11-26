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

        public ProductItemService(IProductItemRepository productItemRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
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

        public async Task<IEnumerable<ProductItem>> GetProductsItemAsync()
        {
            var productItems = (await _productItemRepository.GetAllWithIncludeAsync(p => p.Category)).Where(p => p.IsDelete == false);
            return productItems;
        }

        public async Task<ProductItem> FindOneWithIncludeByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            var productItem = await _productItemRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id&&!p.IsDelete, p => p.Category);
            return productItem;
        }

        public async Task<ProductItem> UpdateAsync(ProductItem productItem)
        {
            await _unitOfWork.Commit();
            return productItem;
        }

        public async Task<ProductItem> FindOneWithoutIncludeByIdAsync(int id)
        {
            var rs = await _productItemRepository.GetSingleByIdAsync(id);
            if (rs != null)
            {
                if (rs.IsDelete)
                {
                    rs = null;
                }
            }
            return rs;
        }
    }
}
