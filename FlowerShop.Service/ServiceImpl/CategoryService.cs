using FlowerShop.Common.MyConst;
using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using FlowerShop.Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IProductItemRepository productItemRepository, IProductCategoryRepository productCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _productItemRepository = productItemRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithHierarchy()
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            var rootCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
            var categoriesHierarchy = new List<Category>();
            foreach (var rootCategory in rootCategories)
            {
                TraverseCategories(rootCategory, categoriesHierarchy);
            }
            return categoriesHierarchy;
        }


        private void TraverseCategories(Category category, List<Category> categories)
        {
            categories.Add(category);

            if (category.SubCategories != null)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    TraverseCategories(subCategory, categories);
                }
            }
        }


        public async Task<Category?> FindOneWithIncludeByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            var result = await _categoryRepository.SingleOrDefaultWithIncludeAsync(
                                                                                   c => c.Id == id,
                                                                                   c => c.SubCategories,
                                                                                   c => c.ParentCategory
                                                                                   );
            return result;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesNotWithHierarchy()
        {
            var allCategories = (await _categoryRepository.GetAllAsync()).Where(c=>c.IsDelete==false);
            return allCategories;
        }





        public async Task<Category> AddAsync(Category category)
        {
            var rs = await _categoryRepository.AddAsync(category);
            if (rs != null)
            {
                await _unitOfWork?.Commit();
            }
            return rs;
        }

        public async Task Update(Category category, List<int> selectedSubCategories)
        {
            List<int> listUnselect = new List<int>();
            foreach (var item in category.SubCategories)
            {
                if (!selectedSubCategories.Contains(item.Id))
                {
                    listUnselect.Add(item.Id);
                }
            }

            foreach (var item in listUnselect)
            {
                var subCategory = await _categoryRepository.GetSingleByIdAsync(item);
                if (subCategory != null)
                {
                    subCategory.ParentCategoryId = null;
                }
            }
            await _unitOfWork.Commit();
        }

        public async Task<PopupViewModel> Delete(int id)
        {
            var category = await _categoryRepository.GetSingleByIdAsync(id);
            if (category == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR,"Lỗi", ConstValues.CoLoiXayRa);
            }

            var cannotDelete = (await _productItemRepository.GetAllAsync()).Where(p => p.IsDelete == false && p.CategoryId == id).Any();
            if (!cannotDelete)
            {
                cannotDelete= (await _productCategoryRepository.GetAllAsync()).Where(p=>p.CategoryId == id).Any();
            }

            if (cannotDelete)
            {
                return new PopupViewModel(PopupViewModel.ERROR,"Lỗi","Không thể xóa do đang có ít nhất 1 sản phẩm thuộc danh mục này");
            }

            category.IsDelete = true;
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Xóa sản phẩm thành công");
        }



        //bổ trợ phần thêm product, chỉ cho phép product thuộc danh mục cuối cùng trong hệ phân cấp tương ứng
        public async Task<IEnumerable<Category>> GetCategoriesWithoutSubCategories()
        {
            var allCategories = (await _categoryRepository.GetAllAsync()).Where(c=>c.IsDelete==false);
            var categoriesWithoutSubCategories = allCategories
                .Where(c => c.SubCategories == null || !c.SubCategories.Any())  
                .ToList();

            return categoriesWithoutSubCategories;
        }

        public async Task<Category?> FindOneWithoutIncludeByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            var result = await _categoryRepository.GetSingleByIdAsync(id);
            if (result != null)
            {
                if (result.IsDelete)
                {
                    result = null;
                }
            }
            return result;
        }

        public async Task<bool> IsDescendantAsync(int childCategoryId, int parentCategoryId)
        {
            var childCategory = await _categoryRepository.GetSingleByIdAsync(childCategoryId);
            while (childCategory != null)
            {
                if (childCategory.ParentCategoryId == parentCategoryId)
                {
                    return true;
                }

                childCategory = await _categoryRepository.GetSingleByIdAsync(childCategory.ParentCategoryId ?? 0);
            }
            return false; 
        }
    }

}
