using FlowerShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Web.Areas.Customer.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent 
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = (await _categoryService.GetAllCategoriesWithHierarchy()).Where(c=>c.IsCategorySell);
            return View(categories); 
        }
    }
}
