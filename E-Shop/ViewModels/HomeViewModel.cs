using E_Shop.Core.Enums;
using E_Shop.Core.Models;
using E_Shop.Enums;
using E_Shop.Events;
using E_Shop.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace E_Shop.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private int _cartItemsCount;
        public int CartItemsCount
        {
            get { return _cartItemsCount; }
            set { SetProperty(ref _cartItemsCount, value); }
        }

        #region [Commands]

        private bool _showFilterMenuItem = false;
        public bool ShowFilterMenuItem
        {
            get { return _showFilterMenuItem; }
            set { SetProperty(ref _showFilterMenuItem, value); }
        }

        public DelegateCommand<string> NavigateToCommand { get; private set; }

        #endregion

        #region [Ctor]

        public HomeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavigateToCommand = new DelegateCommand<string>(NavigateTo);

            SubscribeToEvents();
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
            if (navigationContext.Parameters.ContainsKey("username"))
                Username = navigationContext.Parameters.GetValue<string>("username");
        }

        #endregion

        #region [Methods]

        private void NavigateTo(string url)
        {
            var param = new NavigationParameters
            {
                { "username", Username }
            };
            switch (url)
            {
                case "Cart":
                    _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(UserCartView), param);
                    break;
                case "Home":
                    _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogView), param);
                    break;
                case "Filter":
                    //_eventAggregator.GetEvent<ShowFilterDialogEvent>().Publish();
                    break;
                case "Logout":
                    _regionManager.RequestNavigate(RegionNames.WindowRegion.ToString(), nameof(LoginView));
                    break;
            }
        }

        private void SubscribeToEvents()
        {
            var updateCartEvent = _eventAggregator.GetEvent<UpdateUserCartEvent>();
            updateCartEvent.Subscribe(UpdateUserCart);

            var filterMenuEvent = _eventAggregator.GetEvent<ShowFilterMenuItemEvent>();
            filterMenuEvent.Subscribe((res) => ShowFilterMenuItem = res);
        }

        private void UpdateUserCart(CartItemEventModel model)
        {
            switch (model.Action)
            {
                case CartAction.Add:
                    CartItemsCount += model.Quantity;
                    break;
                case CartAction.Remove:
                    CartItemsCount -= model.Quantity;
                    break;
                case CartAction.Submit:
                    CartItemsCount = 0;
                    break;
            }
        }

        #endregion
    }
}
