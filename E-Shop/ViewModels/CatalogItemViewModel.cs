using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace E_Shop.ViewModels
{
    public class CatalogItemViewModel : BindableBase, INavigationAware
    {
        #region [Properties]

        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

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

        public CatalogItemViewModel(IRegionManager regionManager, IUserService userService, ICartService cartService)
        {
            _regionManager = regionManager;
            _userService = userService;
            _cartService = cartService;

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
            if (navigationContext.Parameters.ContainsKey("product"))
                SelectedProduct = navigationContext.Parameters.GetValue<Product>("product");

            if (navigationContext.Parameters.ContainsKey("username"))
            {
                UserId = _userService.GetUser(navigationContext.Parameters.GetValue<string>("username").ToString()).Id;
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
        }

        #endregion
    }
}
