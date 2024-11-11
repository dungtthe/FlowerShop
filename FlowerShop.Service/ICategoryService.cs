using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAllCategoriesWithHierarchy();
        Task<ICollection<Category>> GetAllCategoriesNotWithHierarchy();
        Task<Category?> GetSingleByIdAsync(int id);
        Task Update(Category category, List<int> selectedSubCategories);
        Task<Category>AddAsync(Category category);
        Task<ResponeMessage> Delete(int id);

    }
}
