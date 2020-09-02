using E_Shop.Core.Entities;
using Prism.Mvvm;

namespace E_Shop.Models
{
    public class CartItemModel : BindableBase
    {
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
    }
}
