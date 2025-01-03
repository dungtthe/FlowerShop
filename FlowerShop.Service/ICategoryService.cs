﻿using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Models;
using FlowerShop.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesWithHierarchy();
        Task<IEnumerable<Category>> GetAllCategoriesNotWithHierarchy();
        Task<Category?> FindOneWithIncludeByIdAsync(int id);
        Task<ResponeMessage> AddAsync(Category category);
        Task<ResponeMessage> DeleteAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesWithoutSubCategories();
        Task<Category?> FindOneWithoutIncludeByIdAsync(int id);
        Task<bool> IsDescendantAsync(int childCategoryId, int parentCategoryId);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<ResponeMessage> UpdateAsync(int categoryId,string nameCategory,int parrentCategoryId, List<int> selectedSubCategories);
    }
}
