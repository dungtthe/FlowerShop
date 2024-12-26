using FlowerShop.Common.Template;
using FlowerShop.DataAccess.Models;
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
        Task<ResponeMessage> UpdateProductInCartAsync(AppUser appUser, int productId, int quantity);
    }
}
