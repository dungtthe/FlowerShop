using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface ICartService
    {
        Task<ResponeMessage> AddProductToCartAsync(AppUser appUser, int productId, int quantity);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<ResponeMessage> DeleteProductFromCartAsync(AppUser appUser,int productId);
        Task<Cart> HandlerQuantityProductInCartAsync(Cart cart);
        Task<ResponeMessage> HandlerCheckoutAsync(AppUser user, int[]? productsId, int[]? quantities);
    }
}
