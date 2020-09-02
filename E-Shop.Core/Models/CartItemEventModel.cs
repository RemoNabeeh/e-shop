using E_Shop.Core.Enums;

namespace E_Shop.Core.Models
{
    public class CartItemEventModel : CartItemModel
    {
        public CartAction Action { get; set; }
    }
}
