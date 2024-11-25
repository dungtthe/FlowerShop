using FlowerShop.DataAccess.Models;
using FlowerShop.Web.ViewModels;
using System.ComponentModel;
using System.Data;

namespace FlowerShop.Web.Mappings
{
    public static class ProductMapping
    {
        public static EditProductViewModel MapToEditProductViewModel(Product product, IEnumerable<ProductItem> productsItem, IEnumerable<Category> categories)
        {
            EditProductViewModel editProductViewModel = new EditProductViewModel();
            editProductViewModel.Id= product.Id;
            editProductViewModel.Title = product.Title;
            editProductViewModel.Description = product.Description;
            editProductViewModel.Quantity = product.Quantity;
            editProductViewModel.PackagingId = product.PackagingId;
           

            //danh sách sản phẩm con có trong sản phẩm này
            editProductViewModel.ProductsItemInProduct = new List<EditProductViewModel.ProductItemViewModel>();
            foreach(var item in product.ProductProductItems)
            {
                editProductViewModel.ProductsItemInProduct.Add(new EditProductViewModel.ProductItemViewModel()
                {
                    Id=item.ProductItemId,
                    Name=item.ProductItem.Name,
                    Quantity = item.Quantity,
                    Price = item.ProductItem.ImportPrice,
                    Images = item.ProductItem.Images,
                });
            }

            //danh sách giá bán
            editProductViewModel.ProductPrices = new List<EditProductViewModel.ProductPriceViewModel>();
            foreach(var item in product.ProductPrices)
            {
                editProductViewModel.ProductPrices.Add(new EditProductViewModel.ProductPriceViewModel()
                {
                    Id=item.Id,
                    Price=item.Price,
                    Priority=item.Priority,
                    StartDate=item.StartDate,
                    EndDate=item.EndDate,
                });
            }

            //danh sách sản phẩm con trong kho
            editProductViewModel.ProductsItemInStock = new List<EditProductViewModel.ProductItemViewModel>();
            foreach (var item in productsItem)
            {
                editProductViewModel.ProductsItemInStock.Add(new EditProductViewModel.ProductItemViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Price = item.ImportPrice,
                    Images = item.Images,
                });
            }


            //danh sách danh mục đang thuộc về và đang có của hệ thống
            editProductViewModel.Categories = new List<EditProductViewModel.CategoryViewModel>();
            foreach (var item in product.ProductCategories)
            {
                editProductViewModel.Categories.Add(new EditProductViewModel.CategoryViewModel()
                {
                    Id = item.CategoryId,
                    Name = item.Category.Name,
                    IsSelect = true
                });
            }
            foreach (var item in categories)
            {

                bool flag = false;
                foreach (var catView in editProductViewModel.Categories)
                {
                    if (item.Id == catView.Id)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    editProductViewModel.Categories.Add(new EditProductViewModel.CategoryViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsSelect = false
                    });
                }
            }

            editProductViewModel.Images= product.Images;

            return editProductViewModel;
            
        }
    }

}
