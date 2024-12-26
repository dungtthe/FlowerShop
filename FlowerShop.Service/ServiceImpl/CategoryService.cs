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
using System.Diagnostics;
using System.Net.WebSockets;

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
            var allCategories = (await _categoryRepository.GetAllAsync()).Where(c => c.IsDelete == false).ToList();



            var categoryDict = allCategories.ToDictionary(c => c.Id);

            // Xây dựng cây phân cấp
            foreach (var category in allCategories)
            {

                if (category.ParentCategoryId.HasValue && categoryDict.ContainsKey(category.ParentCategoryId.Value))
                {
                    // Gắn danh mục vào danh mục cha của nó
                    var parentCategory = categoryDict[category.ParentCategoryId.Value];
                    if (parentCategory.SubCategories == null)
                    {
                        parentCategory.SubCategories = new List<Category>();
                    }
                  
                    parentCategory.SubCategories.Add(category);
                }
            }
            // Trả về danh sách root categories
            var roots = allCategories.Where(c => c.ParentCategoryId == null).ToList();

            foreach(var root in roots)
            {
                if(root.SubCategories!=null && root.SubCategories.Any())
                {
                    root.SubCategories = root.SubCategories.Where(s => !s.IsDelete).ToList();
                }
            }

            return roots;
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

            if (result!=null && result.IsDelete)
            {
                result = null;
            }
            return result;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesNotWithHierarchy()
        {
            var allCategories = (await _categoryRepository.GetAllAsync()).Where(c => c.IsDelete == false);
            return allCategories;
        }



        public async Task<ResponeMessage> AddAsync(Category category)
        {
            if (!category.IsCategorySell && category.ParentCategoryId!=null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Danh mục trong kho không được phép có danh mục cha");
            }
            await _categoryRepository.AddAsync(category);
            await _unitOfWork?.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, "Thêm danh mục thành công");
        }



        public async Task<ResponeMessage> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetSingleByIdAsync(id);
            if (category == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy danh mục");
            }

            var cannotDelete = (await _productItemRepository.GetAllAsync()).Where(p => p.IsDelete == false && p.CategoryId == id).Any();
            if (!cannotDelete)
            {
                cannotDelete = (await _productCategoryRepository.GetAllAsync()).Where(p => p.CategoryId == id && !p.IsDelete).Any();
            }

            if (cannotDelete)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Không thể xóa do đang có ít nhất 1 sản phẩm thuộc danh mục này");
            }
            var categories = (await _categoryRepository.GetAllAsync()).Where(c=>c.Id!=id);
            foreach(var item in categories)
            {
                if (item.ParentCategoryId == id)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Không thể xóa do danh mục này có danh mục con");
                }
            }
            if (!category.IsCategorySell && category.Name == "KHÔNG XÁC ĐỊNH")
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Không thể xóa do danh mục này là danh mục mặc định trong kho");
            }
            category.IsDelete = true;
            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, "Xóa danh mục thành công");

        }



        //bổ trợ phần thêm product, chỉ cho phép product thuộc danh mục cuối cùng trong hệ phân cấp tương ứng
        public async Task<IEnumerable<Category>> GetCategoriesWithoutSubCategories()
        {
            var allCategories = (await _categoryRepository.GetAllAsync()).Where(c => c.IsDelete == false);
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

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

	

		public async Task<ResponeMessage> UpdateAsync(int categoryId, string nameCategory, int parrentCategoryId, List<int> selectedSubCategories)
		{

			#region kiểm tra dữ liệu đầu vào xem có hợp lệ hay k
			var category = await FindOneWithIncludeByIdAsync(categoryId);
            if (category == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR,"Không tìm thấy danh mục");
            }

            foreach (var subCategory in selectedSubCategories)
            {
                var catFind = await _categoryRepository.GetSingleByIdAsync(subCategory);
				if (catFind == null)
				{
					return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy danh mục");
				}

			}
            if (!category.IsCategorySell)
            {
                parrentCategoryId = 0;
            }
            if (parrentCategoryId != 0)
            {
				var catFind = await _categoryRepository.GetSingleByIdAsync(parrentCategoryId);
				if (catFind == null)
				{
					return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy danh mục");
				}
			}

         
			#endregion

			//update tên
			category.Name = nameCategory;

            //update danh mục cha
            if (parrentCategoryId == 0)
            {
                category.ParentCategory = null;
			}
            else
            {
                //danh mục trong kho không được phép có hệ thống phân cấp
                if (!category.IsCategorySell)
                {
					return new ResponeMessage(ResponeMessage.ERROR, "Danh mục trong kho không được phép có hệ thống phân cấp");
				}


                //nếu danh mục hiện tại mà có con rồi thì không cho phép nó làm con của danh mục khác
                var findSub = (await _categoryRepository.GetAllAsync()).Where(c=> c.ParentCategoryId!=null&&c.ParentCategoryId==categoryId);
                if (findSub.Any())
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Không thể thêm danh mục cha, do hệ thống phân cấp tối đa của danh mục bán là 2");
                }


                //danh mục được chọn làm cha mà có sản phẩm thuộc về thì k được phép
                var findProducts = (await _productCategoryRepository.GetAllAsync()).Where(p=>!p.IsDelete && p.CategoryId==parrentCategoryId);
                if (findProducts.Any())
                {
					return new ResponeMessage(ResponeMessage.ERROR, "Không thể thiết lập danh mục cha do danh mục cha được chọn đã có sản phẩm thuộc về");
				}

                //cập nhật danh mục cha
                category.ParentCategoryId = parrentCategoryId;
			}

            //danh mục con được chọn đang là cha của danh mục khác thì không được phép
            foreach(var item in selectedSubCategories)
            {
                var findSubs = (await _categoryRepository.GetAllAsync()).Where(c=>c.ParentCategoryId==item);
                if (findSubs.Any())
                {
					return new ResponeMessage(ResponeMessage.ERROR, "Danh mục con được chọn đang là cha của danh mục khác");
				}
			}


			//hệ thống phân cấp phải cùng thuộc bán hoặc trong kho
			foreach (var item in selectedSubCategories)
			{
                var findCat = await _categoryRepository.GetSingleByIdAsync(item);
                if (findCat.IsCategorySell != category.IsCategorySell)
                {
					return new ResponeMessage(ResponeMessage.ERROR, "Hệ thống phân cấp phải cùng thuộc loại để bán/trong kho.");
				}
			}


			//update subcategories
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
			return new ResponeMessage(ResponeMessage.SUCCESS, "Sửa thông tin danh mục thành công.");

		}
	}

}
