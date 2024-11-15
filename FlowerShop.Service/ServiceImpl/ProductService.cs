using FlowerShop.Common.Template;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IPackagingService _packagingService;
        private readonly ICategoryService _categoryService;
        private readonly IProductItemService _productItemService;
        private readonly IProductProductItemRepository _productProductItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork, IProductPriceRepository productPriceRepository, IPackagingService packagingService, ICategoryService categoryService,IProductItemService productItemService, IProductProductItemRepository productProductItemRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _productPriceRepository = productPriceRepository;
            _packagingService = packagingService;
            _categoryService = categoryService;
            _productItemService = productItemService;
        _productProductItemRepository = productProductItemRepository;
        }

        public async Task<ResponeMessage> AddNewProductAsync(string title, string desc, decimal price, int quantity, int packId, List<int> categoriesId, List<Tuple<int, int>> productsItem)
        {

            //kiểm tra cách đóng gói có hợp lệ hay không
            Packaging rsFindPackaging = await _packagingService.FindOneById(packId);
            if (rsFindPackaging == null)
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
            if (rsAddProductPrice == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
            }



            //add ProductCategory
            foreach (var catId in categoriesId)
            {
                var rsFindCat = await _categoryService.FindOneWithoutIncludeByIdAsync(catId);
                if (rsFindCat == null)
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
            foreach(var productItem in productsItem)
            {
                var rsFindProductItem = await _productItemService.FindOneWithoutIncludeByIdAsync(productItem.Item1);
                if (rsFindProductItem == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Không tìm thấy sản phẩm trong kho");
                }

                //số lượng trong kho với số lượng product muốn bán
                if (productItem.Item2*quantity > rsFindProductItem.Quantity)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Sản phẩm {rsFindProductItem.Name} trong kho có số lượng không đủ");
                }

                var rsAddProductProductItem = await _productProductItemRepository.AddAsync(new ProductProductItem()
                {
                    Product = product,
                    ProductItemId = productItem.Item1,
                });

                if(rsAddProductProductItem == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
                }
                
            }


            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, "Thêm sản phẩm thành công");

        }

   
        public async Task<IEnumerable<Product>> GetProductsForIndexAsync()
        {
            var products = (await _productRepository.GetAllWithIncludeAsync(p => p.Packaging)).Where(p => p.IsDelete == false);
            return products;
        }
    }
}
