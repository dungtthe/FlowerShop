using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class CartService : ICartService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CartService(IAppUserRepository appUserRepository, ICartRepository cartRepository, ICartDetailRepository cartDetailRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _appUserRepository = appUserRepository;
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddProductToCartAsync(AppUser appUser, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                quantity = 1;
            }

            var rsfProduct = await _productRepository.GetSingleByIdAsync(productId);
            if (rsfProduct == null || rsfProduct.IsDelete)
            {
                return new ResponeMessage(ResponeMessage.NOT_FOUND, "");
            }


            var rsfCart = await GetCartByUserIdAsync(appUser.Id);
            if (rsfCart == null)
            {
                return new ResponeMessage(ResponeMessage.NOT_FOUND, "");
            }


            var cartDetailOld = rsfCart.CartDetails.FirstOrDefault(c => c.ProductId == productId);
            string msg = "new";
            //thêm mới
            if (cartDetailOld == null)
            {
                rsfCart.CartDetails.Add(new CartDetail()
                {
                    Cart = rsfCart,
                    Product = rsfProduct,
                    IsDeleted = false,
                    Quantity = quantity
                });
            }
            else//update số lượng
            {
                if (!cartDetailOld.IsDeleted)
                {
                    msg = "old";
                }
                cartDetailOld.IsDeleted = false;
                cartDetailOld.Quantity += quantity;
            }

            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, msg);
        }

        public async Task<ResponeMessage> DeleteProductFromCartAsync(AppUser appUser, int productId)
        {
           
            var rsfCart = await GetCartByUserIdAsync(appUser.Id);
            if (rsfCart == null)
            {
                return new ResponeMessage(ResponeMessage.NOT_FOUND, "");
            }

            var cartDetailOld = rsfCart.CartDetails.FirstOrDefault(c => c.ProductId == productId);
            if (cartDetailOld == null)
            {
                return new ResponeMessage(ResponeMessage.NOT_FOUND, "");
            }
            else
            {
                cartDetailOld.IsDeleted = true;
            }
            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, "");
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var user = await _appUserRepository.SingleOrDefaultWithIncludeAsync(u => u.Id == userId, u => u.Cart);

            if (user?.Cart != null)
            {
                user.Cart.CartDetails = (await _cartDetailRepository.FindAsync(c => c.CartId == user.Cart.Id)).ToList();
                foreach(var item in user.Cart.CartDetails)
                {
                    var product = await _productRepository.GetSingleByIdAsync(item.ProductId);
                    if(product==null || product.IsDelete)
                    {
                        item.IsDeleted = true;
                    }
                }
                await _unitOfWork.Commit();
            }
            return user?.Cart;
        }

        public async Task<ResponeMessage> HandlerCheckoutAsync(AppUser user, int[]? productsId, int[]? quantities)
        {
            if(productsId==null || quantities==null || productsId.Length==0 || quantities.Length==0 || productsId.Length != quantities.Length)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Thông tin sản phẩm chọn và số lượng không hợp lệ");
            }

            var cart =await  _cartRepository.SingleOrDefaultWithIncludeAsync(c=>c.Id==user.CartId, c=>c.CartDetails);
            if(cart==null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Có lỗi xảy ra");
            }

            //check xem product và số lượng có hợp lệ hay không

            string sProductIds = "";
            for(int i =0; i < productsId.Length; i++)
            {
                sProductIds += productsId[i]+ " ";
                var findProduct = await _productRepository.GetSingleByIdAsync(productsId[i]);
                if (findProduct == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy sản phẩm đã chọn");
                }
                if (findProduct.IsDelete)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Sản phẩm {findProduct.Title} không còn bán nữa");
                }
                if (findProduct.Quantity < quantities[i])
                {
                    return new ResponeMessage(ResponeMessage.ERROR, $"Sản phẩm {findProduct.Title} không đủ số lượng");
                }

                var cartDetail =  cart.CartDetails.Where(c=>c.ProductId== findProduct.Id && !c.IsDeleted).FirstOrDefault();
                if (cartDetail == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Có lỗi xảy ra");
                }
                cartDetail.Quantity = quantities[i];
            }
            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, sProductIds);
        }

        public async Task<Cart> HandlerQuantityProductInCartAsync(Cart cart)
        {
            cart.CartDetails = (await _cartDetailRepository.FindAsync(c => c.CartId == cart.Id)).ToList();
            foreach (var item in cart.CartDetails)
            {
                var product = await _productRepository.GetSingleByIdAsync(item.ProductId);
                if (product == null || product.IsDelete || product.Quantity<=0)
                {
                    item.IsDeleted = true;
                    continue;
                }
                if (item.Quantity > product.Quantity)
                {
                    item.Quantity = product.Quantity;
                }
            }
            await _unitOfWork.Commit();
            return cart;
        }
    }
}
