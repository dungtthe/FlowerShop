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

        public async Task<ResponeMessage> AddProductToCart(AppUser appUser, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                quantity = 1;
            }

            var rsfProduct = await _productRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == productId, pp => pp.ProductPrices);
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
            bool isNew = false;
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
                isNew = true;
            }
            else//update số lượng
            {
                cartDetailOld.IsDeleted = false;
                cartDetailOld.Quantity += quantity;
            }

            await _unitOfWork.Commit();
            return new ResponeMessage(ResponeMessage.SUCCESS, isNew ? "new" : "old");
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var user = await _appUserRepository.SingleOrDefaultWithIncludeAsync(u => u.Id == userId, u => u.Cart);

            if (user?.Cart != null)
            {
                user.Cart.CartDetails = (await _cartDetailRepository.FindAsync(c => c.CartId == user.Cart.Id)).ToList();
            }
            return user?.Cart;
        }
    }
}
