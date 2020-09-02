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

namespace E_Shop.ViewModels
{
    public class CatalogItemViewModel : BindableBase, INavigationAware
    {
        #region [Properties]

        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly IStringsResourceService _stringsResourceService;


        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        #endregion

        #region [Commands]

        public DelegateCommand<string> UpdateQuantityCommand { get; private set; }
        public DelegateCommand AddToCartCommand { get; set; }

        #endregion

        #region [Ctor]

        public CatalogItemViewModel(IRegionManager regionManager, IUserService userService, ICartService cartService, 
            IEventAggregator eventAggregator, IDialogService dialogService, IStringsResourceService stringsResourceService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _userService = userService;
            _cartService = cartService;
            _dialogService = dialogService;
            _stringsResourceService = stringsResourceService;

            UpdateQuantityCommand = new DelegateCommand<string>(UpdateQuantity);
            AddToCartCommand = new DelegateCommand(AddToCart);
        }

        #endregion

        #region [Navigation]

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Quantity = 1;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<ShowFilterMenuItemEvent>().Publish(false);

            if (navigationContext.Parameters.ContainsKey(Constants.Product))
            {
                SelectedProduct = navigationContext.Parameters.GetValue<Product>(Constants.Product);
            }

            if (navigationContext.Parameters.ContainsKey(Constants.Username))
            {
                UserId = _userService.GetUser(navigationContext.Parameters.GetValue<string>(Constants.Username).ToString()).Id;
            }
        }

        #endregion

        #region [Methods]

        private void UpdateQuantity(string operation)
        {
            switch (operation)
            {
                case "Inc":
                    Quantity++;
                    break;
                case "Dec":
                    if (Quantity > 1)
                        Quantity--;
                    break;
            }
        }

        private void AddToCart()
        {
            _cartService.AddToCart(new Cart()
            {
                UserId = UserId,
                ProductId = SelectedProduct.Id,
                Quantity = Quantity  
            });

            var eventPayload = new CartItemEventModel
            {
                Product = SelectedProduct,
                Quantity = Quantity,
                Action = CartAction.Add
            };

            _eventAggregator.GetEvent<UpdateUserCartEvent>().Publish(eventPayload);

            _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogView));

            var dialogParams = new DialogParameters
            {
                { Constants.Message,  _stringsResourceService.GetMessage(Constants.CartUpdated) }
            };

            _dialogService.ShowDialog(nameof(MessageDialogView), dialogParams, (res) => { });
        }

        #endregion
    }
}