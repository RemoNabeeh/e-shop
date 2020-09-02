using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Enums;
using E_Shop.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace E_Shop.ViewModels
{
    public class CatalogViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IProductService _productService;
        private readonly IEventAggregator _eventAggregator;

        #region [Properties]

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        #endregion

        #region [Commands]

        public DelegateCommand<Product> NavigateToProductCommand { get; set; }

        #endregion

        #region [Ctor]

        public CatalogViewModel(
            IRegionManager regionManager,
            IDialogService dialogService,
            IProductService productService,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            _productService = productService;
            _eventAggregator = eventAggregator;


            NavigateToProductCommand = new DelegateCommand<Product>(NavigateToProduct);
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

            LoadProducts();
        }

        #endregion

        #region [Methods]

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_productService.GetAll(e => true));
        }

        private void NavigateToProduct(Product product)
        {
            if (product == null)
                return;

            var param = new NavigationParameters
            {
                { "username", Username},
                { "product", product }
            };

            _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogItemView), param);
        }

        #endregion
    }
}
