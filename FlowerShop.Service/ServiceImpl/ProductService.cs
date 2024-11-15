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
        private readonly IPackagingRepository _packagingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductProductItemRepository _productProductItemRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork, IProductPriceRepository productPriceRepository, IPackagingRepository packagingRepository, ICategoryRepository categoryRepository, IProductItemRepository productItemRepository, IProductProductItemRepository productProductItemRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _productPriceRepository = productPriceRepository;
            _packagingRepository = packagingRepository;
            _categoryRepository = categoryRepository;
            _productItemRepository = productItemRepository;
            _productProductItemRepository = productProductItemRepository;
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
            if (rsAddProductPrice == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
            }



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
                var rsFindProductItem =  await _productItemRepository.GetSingleByIdAsync(productItem.Item1);
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
                rsFindProductItem.Quantity=rsFindProductItem.Quantity-productItem.Item2*quantity;

                 _productItemRepository.Update(rsFindProductItem);


                var rsAddProductProductItem = await _productProductItemRepository.AddAsync(new ProductProductItem()
                {
                    Product = product,
                    ProductItemId = productItem.Item1,
                });

                if (rsAddProductProductItem == null)
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
