using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace E_Shop.Business
{
    public class CartService : ICartService
    {
        private List<Cart> _carts;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        
        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;

            _carts = new List<Cart>();
        }

        public void AddToCart(Cart cart)
        {
            var userCart = _carts.FirstOrDefault(e => e.UserId == cart.UserId && e.ProductId == cart.ProductId);
            if (userCart != null)
            {
                userCart.Quantity += cart.Quantity;
            }
            else
            {
                _carts.Add(cart);
            }
        }

        public IEnumerable<CartItemModel> GetUserCart(int userId)
        {
            return _carts
                .Where(c => c.UserId == userId)
                .Select(e =>
            {
                return new CartItemModel
                {
                    Product = _productRepository.GetProduct(e.ProductId),
                    Quantity = e.Quantity
                };
            });
        }

        public int GetCartCount(int userId)
        {
            return _carts.Count(c => c.UserId == userId);
        }

        public void RemoveFromCart(int userId, int productId)
        {
            var cartItem = _carts.FirstOrDefault(e => e.UserId == userId && e.ProductId == productId);
            if (cartItem != null)
                _carts.Remove(cartItem);
        }

        public void SubmitCart()
        {
            ClearCart();
        }

        public void ClearCart()
        {
            _carts = new List<Cart>();
        }
    }
}
