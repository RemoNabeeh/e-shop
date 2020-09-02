using E_Shop.Core.Entities;
using E_Shop.Core.Models;
using System.Collections.Generic;

namespace E_Shop.Core.Interfaces.Services
{
    public interface ICartService
    {
        void AddToCart(Cart cart);
        IEnumerable<CartItemModel> GetUserCart(int userId);
        int GetCartCount(int userId);
        void RemoveFromCart(int userId, int productId);
        void SubmitCart();
        void ClearCart();
    }
}
