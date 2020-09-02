using E_Shop.Core.Entities;
using E_Shop.Core.Enums;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Core.Models;
using E_Shop.Dialogs.Views;
using E_Shop.Enums;
using E_Shop.Events;
using E_Shop.Helpers;
using E_Shop.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;

namespace E_Shop.ViewModels
{
    public class UserCartViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IStringsResourceService _stringsResourceService;

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

        public DelegateCommand<CartItemModel> RemoveCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        #endregion

        #region [Ctor]

        public UserCartViewModel(IUserService userService, ICartService cartService, IEventAggregator eventAggregator, IRegionManager regionManager, IDialogService dialogService, IStringsResourceService stringsResourceService)
        {
            _userService = userService;
            _cartService = cartService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _dialogService = dialogService;
            _stringsResourceService = stringsResourceService;

            RemoveCommand = new DelegateCommand<CartItemModel>(Remove);
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
            CheckCartIsEmpty();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterMenuItemEvent>().Publish(false);

            if (!navigationContext.Parameters.ContainsKey(Constants.Username))
                return;

            var username  = navigationContext.Parameters.GetValue<string>(Constants.Username);
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

        private void Remove(CartItemModel model)
        {
            _cartService.RemoveFromCart(UserId, model.Product.Id);
            Items.Remove(model);
            CheckCartIsEmpty();

            var eventPayload = new CartItemEventModel
            {
                Product = model.Product,
                Quantity = model.Quantity,
                Action = CartAction.Remove
            };

            _eventAggregator.GetEvent<UpdateUserCartEvent>().Publish(eventPayload);
        }

        private void Submit()
        {
            _cartService.SubmitCart();
            Items = new ObservableCollection<CartItemModel>();

            var eventPayload = new CartItemEventModel
            {
                Action = CartAction.Submit
            };
            _eventAggregator.GetEvent<UpdateUserCartEvent>().Publish(eventPayload);

            _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogView));

            var dialogParams = new DialogParameters
            {
                { Helpers.Constants.Message, _stringsResourceService.GetString(Helpers.Constants.OrderSubmitted) }
            };

            _dialogService.ShowDialog(nameof(MessageDialogView), dialogParams, (res) => { });
        }

        private void CheckCartIsEmpty()
        {
            EmptyCart = Items.Count == 0;
        }

        #endregion
    }
}
