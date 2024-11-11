using FlowerShop.Common;
using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Category>> GetAllCategoriesWithHierarchy()
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


        public async Task<Category?> GetSingleByIdAsync(int id)
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

        public async Task<ICollection<Category>> GetAllCategoriesNotWithHierarchy()
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return allCategories.ToList();
        }

   

      

        public async Task<Category> AddAsync(Category category)
        {
            var rs = await _categoryRepository.AddAsync(category);
            if (rs != null)
            {
                _unitOfWork?.Commit();
            }
            return rs;
        }

        public async Task Update(Category category, List<int> selectedSubCategories)
        {
            List<int> listUnselect = new List<int>();
            foreach(var item in category.SubCategories)
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
            _unitOfWork.Commit();
        }

        public async Task<ResponeMessage> Delete(int id)
        {
            var category =  await _categoryRepository.GetSingleByIdAsync(id);
            if (category == null)
            {
                return new ResponeMessage(0, ConstValues.CoLoiXayRa);
            }
            category.IsDelete = true;
            _unitOfWork.Commit();
            return new ResponeMessage(1, ConstValues.ThanhCong);
        }
    }

}
