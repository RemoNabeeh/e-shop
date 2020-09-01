using E_Shop.Cart.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E_Shop.Cart
{
    public class CartModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public CartModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(UserCartView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
