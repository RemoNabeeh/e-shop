using E_Shop.Business;
using E_Shop.Catalog.Views;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Infrastructure.Repositories;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E_Shop.Catalog
{
    public class CatalogModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public CatalogModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(ProductListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductListView>();
            containerRegistry.RegisterForNavigation<ProductDetailsView>();


            containerRegistry.RegisterSingleton<IProductRepository, ProductRepository>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();
        }
    }
}
