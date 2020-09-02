using E_Shop.Core.Entities;

namespace E_Shop.Core.Models
{
    public class CartItemModel
    {
        public Product Product { get; set;}
        public int Quantity { get; set;}
    }
}
