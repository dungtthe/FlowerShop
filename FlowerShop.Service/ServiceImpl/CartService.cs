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

            var rsfProduct = await _productRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == productId && !p.IsDelete, pp => pp.ProductPrices);
            if (rsfProduct == null)
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
    }
}
