using FlowerShop.Common.Template;
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
        Task Update(Category category, List<int> selectedSubCategories);
        Task<Category>AddAsync(Category category);
        Task<PopupViewModel> Delete(int id);
        Task<IEnumerable<Category>> GetCategoriesWithoutSubCategories();
        Task<Category?> FindOneWithoutIncludeByIdAsync(int id);
        Task<bool> IsDescendantAsync(int childCategoryId, int parentCategoryId);

    }
}
