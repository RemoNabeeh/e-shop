using E_Shop.Core.Enums;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Core.Models;
using E_Shop.Dialogs.ViewModels;
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
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

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

        private bool _showFilterMenuItem = false;
        public bool ShowFilterMenuItem
        {
            get { return _showFilterMenuItem; }
            set { SetProperty(ref _showFilterMenuItem, value); }
        }

        #region [Commands]
        
        public DelegateCommand<string> NavigateToCommand { get; private set; }

        #endregion

        #region [Ctor]

        public HomeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

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
                { Constants.Username, Username }
            };
            switch (url)
            {
                case Constants.CartNavigation:
                    _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(UserCartView), param);
                    break;
                case Constants.HomeMenuItem:
                    _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogView), param);
                    break;
                case Constants.FilterMenuItem:
                    _dialogService.ShowDialog(nameof(FilterDialogView), null, result =>
                    {
                        if (result.Result == ButtonResult.OK)
                        {
                            var payload = new ApplyFilterModel()
                            {
                                MinValue = result.Parameters.GetValue<double>(Constants.FilterMinValue),
                                MaxValue = result.Parameters.GetValue<double>(Constants.FilterMaxnValue)
                            };
                            _eventAggregator.GetEvent<ApplyFilterEvent>().Publish(payload);
                        }   
                    });
                    break;
                case Constants.LogoutMenuItem:
                    _regionManager.RequestNavigate(RegionNames.WindowRegion.ToString(), nameof(LoginView));
                    break;
            }
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<UpdateUserCartEvent>().Subscribe(UpdateUserCart);
            _eventAggregator.GetEvent<ShowFilterMenuItemEvent>().Subscribe((res) => ShowFilterMenuItem = res);
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
