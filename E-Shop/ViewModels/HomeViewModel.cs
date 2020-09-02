using E_Shop.Enums;
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

        public DelegateCommand<string> NavigateToCommand { get; private set; }

        #endregion

        #region [Ctor]

        public HomeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavigateToCommand = new DelegateCommand<string>(NavigateTo);
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

        #endregion
    }
}
