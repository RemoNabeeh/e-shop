using E_Shop.Core.Entities;
using E_Shop.Core.Interfaces.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace E_Shop.Catalog.ViewModels
{
    public class ProductListViewModel : BindableBase
    {
        #region [Properties]

        private readonly IProductService _productService;
        private readonly IRegionManager _regionManager;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        #endregion

        #region [Commands]

        public DelegateCommand<Product> NavigateToProductDetailsCommand { get; private set; }

        #endregion

        #region [Ctor]

        public ProductListViewModel(IRegionManager regionManager, IProductService productService)
        {
            _regionManager = regionManager;
            _productService = productService;

            NavigateToProductDetailsCommand = new DelegateCommand<Product>(NavigateToProductDetails);

            LoadProducts();
        }

        #endregion

        #region [Methods]

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_productService.GetAll(e => true));
        }

        private void NavigateToProductDetails(Product product)
        {
            if (product == null)
                return;

            var param = new NavigationParameters();
            param.Add("product", product);

            _regionManager.RequestNavigate("ContentRegion", "ProductDetailsView", param);
        }

        #endregion
    }
}
