using E_Shop.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace E_Shop.Catalog.ViewModels
{
    public class ProductDetailsViewModel : BindableBase, INavigationAware
    {
        #region [Properties]

        private readonly IRegionManager _regionManager;

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        private int _quantity;
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

        public ProductDetailsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            UpdateQuantityCommand = new DelegateCommand<string>(UpdateQuantity);
            AddToCartCommand = new DelegateCommand(AddToCart);
        }

        #endregion

        #region [Navigation]

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("product"))
                SelectedProduct = navigationContext.Parameters.GetValue<Product>("product");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
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

        }

        #endregion
    }
}
