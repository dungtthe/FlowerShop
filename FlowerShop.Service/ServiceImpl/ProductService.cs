using FlowerShop.Common.MyConst;
using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IPackagingRepository _packagingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductProductItemRepository _productProductItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleInvoiceDetailRepository _saleInvoiceDetailRepository;


        public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork, IProductPriceRepository productPriceRepository, IPackagingRepository packagingRepository, ICategoryRepository categoryRepository, IProductItemRepository productItemRepository, IProductProductItemRepository productProductItemRepository, ISaleInvoiceDetailRepository saleInvoiceDetailRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _productPriceRepository = productPriceRepository;
            _packagingRepository = packagingRepository;
            _categoryRepository = categoryRepository;
            _productItemRepository = productItemRepository;
            _productProductItemRepository = productProductItemRepository;
            _saleInvoiceDetailRepository = saleInvoiceDetailRepository;
        }

        public async Task<ResponeMessage> AddNewProductAsync(string title, string desc, decimal price, int quantity, int packId, List<int> categoriesId, List<Tuple<int, int>> productsItem)
        {

            //kiểm tra cách đóng gói có hợp lệ hay không
            Packaging? rsFindPackaging = await _packagingRepository.GetSingleByIdAsync(packId);
            if (rsFindPackaging == null || rsFindPackaging.IsDelete)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy cách đóng gói tương ứng");
            }


            //add product
            Product product = new Product()
            {
                Title = title,
                Description = desc,
                Quantity = quantity,
                PackagingId = packId,
            };
            var rsAddProduct = await _productRepository.AddAsync(product);
            if (rsAddProduct == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
            }



            //add productPrice
            ProductPrice productPrice = new ProductPrice()
            {
                Product = product,
                Priority = 1,
                Price = price,
            };

            var rsAddProductPrice = await _productPriceRepository.AddAsync(productPrice);



            //add ProductCategory
            foreach (var catId in categoriesId)
            {
                var rsFindCat = await _categoryRepository.GetSingleByIdAsync(catId);
                if (rsFindCat == null || rsFindCat.IsDelete)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Không tìm thấy danh mục");
                }

                var rsAddProductCategory = await _productCategoryRepository.AddAsync(new ProductCategory()
                {
                    Product = product,
                    CategoryId = catId
                });

                if (rsAddProductCategory == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
                }

            }


            //add ProductProductItem
            foreach (var productItem in productsItem)
            {
                var rsFindProductItem = await _productItemRepository.GetSingleByIdAsync(productItem.Item1);
                if (rsFindProductItem == null || rsFindProductItem.IsDelete)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Không tìm thấy sản phẩm trong kho");
                }

                //số lượng trong kho với số lượng product muốn bán
                if (productItem.Item2 * quantity > rsFindProductItem.Quantity)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Sản phẩm {rsFindProductItem.Name} trong kho có số lượng không đủ");
                }

                //cập nhật lại số lượng trong kho
                rsFindProductItem.Quantity = rsFindProductItem.Quantity - productItem.Item2 * quantity;

                _productItemRepository.Update(rsFindProductItem);


                var rsAddProductProductItem = await _productProductItemRepository.AddAsync(new ProductProductItem()
                {
                    Product = product,
                    ProductItemId = productItem.Item1,
                    Quantity = productItem.Item2
                });

                if (rsAddProductProductItem == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
                }

            }


            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, "Thêm sản phẩm thành công");

        }

        public async Task<Product?> FindOneByIdAsync(int id, bool include = true)
        {
            if (id == -1) return null;

            if (include)
            {

                var rs = await _productRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id,
                    p => p.Packaging,
                    p => p.ProductPrices.Where(pp => !pp.IsDelete),
                    p => p.ProductProductItems.Where(pp => !pp.IsDelete),
                    p => p.ProductCategories.Where(pc => !pc.IsDelete)
                    );

                if (rs == null || rs.ProductProductItems == null || rs.ProductCategories == null)
                {
                    return null;
                }
                foreach (var item in rs.ProductProductItems)
                {
                    item.ProductItem = await _productItemRepository.GetSingleByIdAsync(item.ProductItemId);
                    if (item.ProductItem == null)
                    {
                        return null;
                    }
                }

                foreach (var item in rs.ProductCategories)
                {
                    item.Category = await _categoryRepository.GetSingleByIdAsync(item.CategoryId);
                    if (item.Category == null)
                    {
                        return null;
                    }
                }
                return rs;
            }
            else
            {
                return await _productRepository.GetSingleByIdAsync(id);
            }
        }

        public async Task<IEnumerable<Product>> GetTopSellingProductsAsync()
        {
            var productIds = await _saleInvoiceDetailRepository.GetProductIdTopSellingProductsAsync(10);
            if (productIds == null)
            {
                return null;
            }

            List<Product> resultProducts = new List<Product>();
            foreach (var id in productIds)
            {

                var rsp = await _productRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id, p => p.ProductPrices);
                if (rsp != null)
                {
                    resultProducts.Add(rsp);
                }
            }
            if (resultProducts.Count < 10)
            {
                var products = (await _productRepository.GetAllWithIncludeAsync(p => p.ProductPrices)).Where(p => !p.IsDelete);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (resultProducts.Count >= 10)
                        {
                            break;
                        }
                        if (!productIds.Contains(product.Id))
                        {
                            resultProducts.Add(product);
                        }
                    }
                }
            }
            return resultProducts;
        }

        public async Task<(IEnumerable<Product> products, int total, int remaining)> GetGiftCategoryProductsAsync(int pageIndex = 0, int pageSize = 10)
        {
            var giftCategory = await _categoryRepository.SingleOrDefaultWithIncludeAsync(
                c => c.Name == ConstValues.QUA_TANG_KEM && !c.IsDelete,
                c => c.SubCategories.Where(s => !s.IsDelete)
            );

            if (giftCategory == null)
                return (Enumerable.Empty<Product>(), 0, 0);

            return await _productRepository.GetMultiPagingAsync(
                p => (!p.IsDelete) &&
                    p.ProductCategories.Any(pc => !pc.IsDelete &&
                        giftCategory.SubCategories.Select(sc => sc.Id).Contains(pc.CategoryId)),
                pageIndex,
                pageSize,
                null,
                p => p.ProductPrices
            );
        }



        public async Task<(IEnumerable<Product> products, int total, int remaining)> GetNewProductsAsync(int pageIndex = 0, int pageSize = 10)
        {
            return await _productRepository.GetMultiPagingAsync(
                p => !p.IsDelete,
                pageIndex,
                pageSize,
                q => q.OrderByDescending(p => p.Id),
                p => p.ProductPrices
            );
        }

        public async Task<IEnumerable<Product>> GetProductsForIndexInAdminAsync()
        {
            var products = (await _productRepository.GetAllWithIncludeAsync(p => p.Packaging)).Where(p => p.IsDelete == false);
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsForIndexInCustomerAsync()
        {
            var products = (await _productRepository.GetAllWithIncludeAsync(
                p => p.ProductPrices.Where(pp => !pp.IsDelete).OrderBy(pp => pp.Priority)
                )).Where(p => p.IsDelete == false);
            return products;
        }

        public async Task<Product> UpdateImageAsync(Product product)
        {
            var rsFindProduct = await _productRepository.GetSingleByIdAsync(product.Id);
            if (rsFindProduct == null)
            {
                return null;
            }
            rsFindProduct.Images = product.Images;
            _productRepository.Update(rsFindProduct);
            await _unitOfWork.Commit();
            return rsFindProduct;
        }


        public async Task<ResponeMessage> UpdateProductAsync(Product productOld, int quantityNew, List<ProductPrice> productPrices, List<int> categoriesId, List<Tuple<int, int>> productsItem)
        {
            var rsProductPricesOld = (await _productPriceRepository.GetAllAsync())?.Where(pp => pp.ProductId == productOld.Id);
            var rsProductCategoriesOld = (await _productCategoryRepository.GetAllAsync())?.Where(pc => pc.ProductId == productOld.Id);
            var rsProductProductItemsOld = (await _productProductItemRepository.GetAllAsync())?.Where(pi => pi.ProductId == productOld.Id);
            var rsProductItems = (await _productItemRepository.GetAllAsync())?.Where(p => !p.IsDelete);
            if (rsProductPricesOld == null || rsProductCategoriesOld == null || rsProductCategoriesOld == null || rsProductItems == null || rsProductProductItemsOld == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, ResponeMessage.CO_LOI_XAY_RA);
            }


            //cập nhật giá bán
            //chuyển toàn bộ giá bán ban đầu về trạng thái đã xóa
            foreach (var item in rsProductPricesOld)
            {
                item.IsDelete = true;
            }
            foreach (var item in productPrices)
            {
                var productPriceOld = rsProductPricesOld.FirstOrDefault(p => p.Price == item.Price);
                //chưa có thì tạo mới
                if (productPriceOld == null)
                {
                    ProductPrice productPrice = new ProductPrice()
                    {
                        ProductId = productOld.Id,
                        Priority = item.Priority,
                        Price = item.Price,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        IsDelete = false
                    };
                    await _productPriceRepository.AddAsync(productPrice);
                }
                else//có rồi thì update
                {
                    productPriceOld.IsDelete = false;
                    productPriceOld.Price = item.Price;
                    productPriceOld.Priority = item.Priority;
                    productPriceOld.StartDate = item.StartDate;
                    productPriceOld.EndDate = item.EndDate;
                }
            }


            //cập nhật danh mục thuộc về
            foreach (var item in rsProductCategoriesOld)
            {
                item.IsDelete = true;
            }
            foreach (var item in categoriesId)
            {
                var categoryOld = rsProductCategoriesOld.FirstOrDefault(c => c.CategoryId == item);
                //chưa có thì thêm mới
                if (categoryOld == null)
                {
                    await _productCategoryRepository.AddAsync(new ProductCategory()
                    {
                        ProductId = productOld.Id,
                        CategoryId = item,
                        IsDelete = false
                    });
                }
                else//có rồi thì update
                {
                    categoryOld.IsDelete = false;
                }
            }



            //cập nhật sản phẩm bao gồm
            //ném hết sản phẩm bao gồm đang có vào kho
            foreach (var item in rsProductProductItemsOld)
            {
                var productItem = rsProductItems.FirstOrDefault(p => p.Id == item.ProductItemId);
                if (productItem == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, ResponeMessage.CO_LOI_XAY_RA);
                }
                item.IsDelete = true;
                productItem.Quantity += item.Quantity * productOld.Quantity;
                item.Quantity = 0;
            }
            //thêm lại sản phẩm bao gồm
            foreach (var item in productsItem)
            {
                var productItem = rsProductItems.FirstOrDefault(p => p.Id == item.Item1);
                if (productItem == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, ResponeMessage.CO_LOI_XAY_RA);
                }
                if ((productItem.Quantity - item.Item2 * quantityNew) < 0)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"{productItem.Name} không đủ số lượng");
                }
                productItem.Quantity = productItem.Quantity - item.Item2 * quantityNew;

                //xem đã có productproductitem chưa
                var ppi = rsProductProductItemsOld.FirstOrDefault(p => p.ProductItemId == item.Item1);
                //chưa thì thêm mới
                if (ppi == null)
                {
                    await _productProductItemRepository.AddAsync(new ProductProductItem()
                    {
                        ProductId = productOld.Id,
                        ProductItemId = item.Item1,
                        Quantity = item.Item2,
                        IsDelete = false
                    });
                }
                else
                {
                    ppi.Quantity = item.Item2;
                    ppi.IsDelete = false;
                }
            }
            productOld.Quantity = quantityNew;
            await _unitOfWork.Commit();

            return new ResponeMessage(ResponeMessage.SUCCESS, "Sửa thông tin sản phẩm thành công");
        }


        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.SingleOrDefaultWithIncludeAsync(
                p => p.Id == productId && !p.IsDelete,
                p => p.ProductCategories,
                p => p.ProductPrices
            );
        }

        public async Task<IEnumerable<Category>> GetCategoriesByProductIdAsync(int productId)
        {
            // Lấy tất cả các ProductCategories không bị xóa
            var productCategories = await _productCategoryRepository.GetAllAsync();

            // Lọc các ProductCategory liên kết với sản phẩm và chưa bị xóa
            var productCategoryIds = productCategories
                .Where(pc => pc.ProductId == productId && !pc.IsDelete)
                .Select(pc => pc.CategoryId)
                .ToList();

            // Lấy tất cả các danh mục chưa bị xóa
            var categories = await _categoryRepository.GetAllAsync();

            // Lọc các danh mục có CategoryId trong danh sách của ProductCategories
            var filteredCategories = categories
                .Where(c => productCategoryIds.Contains(c.Id) && !c.IsDelete)
                .ToList();

            return filteredCategories;
        }

        public async Task<Packaging> GetPackagingByIdAsync(int packagingId)
        {
            return await _packagingRepository.SingleOrDefaultAsync(p => p.Id == packagingId);
        }



    }
}
