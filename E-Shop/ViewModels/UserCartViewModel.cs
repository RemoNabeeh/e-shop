using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace E_Shop.ViewModels
{
    public class UserCartViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private bool _emptyCart;
        public bool EmptyCart
        {
            get { return _emptyCart; }
            set { SetProperty(ref _emptyCart, value); }
        }

        private ObservableCollection<CartItemModel> _items;
        public ObservableCollection<CartItemModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #region [Commands]

        public DelegateCommand<CartItemModel> DeleteCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        #endregion

        #region [Ctor]

        public UserCartViewModel(IUserService userService, ICartService cartService)
        {
            _userService = userService;
            _cartService = cartService;


            DeleteCommand = new DelegateCommand<CartItemModel>(Delete);
            SubmitCommand = new DelegateCommand(Submit);
        }

        #endregion

        #region [Navigation]

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!navigationContext.Parameters.ContainsKey("username"))
                return;

            var username  = navigationContext.Parameters.GetValue<string>("username");
            UserId = _userService.GetUser(username).Id;

            GetUserCart(UserId);
        }

        #endregion

        #region [Methods]

        private void GetUserCart(int userId)
        {
            var userCart = _cartService.GetUserCart(userId);
            Items = new ObservableCollection<CartItemModel>(userCart.Select(e =>
            {
                return new CartItemModel
                {
                    Quantity = e.Quantity,
                    Product = new Product { Id = e.Product.Id, Name = e.Product.Name, Price = e.Product.Price * e.Quantity, InStock = e.Product.InStock, Description = e.Product.Description, Images = e.Product.Images }
                };
            }));
            CheckCartIsEmpty();
        }

        private void Delete(CartItemModel model)
        {
            _cartService.RemoveFromCart(UserId, model.Product.Id);
            Items.Remove(model);
            CheckCartIsEmpty();
        }

        private void Submit()
        {

        }

        private void CheckCartIsEmpty()
        {
            EmptyCart = Items.Count == 0;
        }

        #endregion
    }
}
