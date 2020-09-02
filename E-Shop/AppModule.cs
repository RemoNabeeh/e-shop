using E_Shop.Business;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Dialogs;
using E_Shop.Enums;
using E_Shop.Infrastructure.Repositories;
using E_Shop.ViewModels;
using E_Shop.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E_Shop
{
    public class AppModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public AppModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.WindowRegion.ToString(), typeof(LoginView));
            _regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion.ToString(), typeof(HeaderView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion.ToString(), typeof(CatalogView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterDependencyInjectionTypes(containerRegistry);
            RegisterForNavigation(containerRegistry);
            RegisterDialogs(containerRegistry);
        }

        #region [Register For Navigation]

        private void RegisterForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<CatalogView, CatalogViewModel>();
            containerRegistry.RegisterForNavigation<CatalogItemView, CatalogItemViewModel>();
            containerRegistry.RegisterForNavigation<UserCartView, UserCartViewModel>();
        }

        #endregion

        #region [Register Dependency Injection]

        private void RegisterDependencyInjectionTypes(IContainerRegistry containerRegistry)
        {
            #region [Repositories]

            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<IProductRepository, ProductRepository>();
            containerRegistry.RegisterSingleton<ICartRepository, CartRepository>();

            #endregion

            #region [Services]

            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();
            containerRegistry.RegisterSingleton<ICartService, CartService>();

            #endregion
        }

        #endregion

        #region [Register Dialogs]

        private void RegisterDialogs(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();
            containerRegistry.RegisterDialog<FilterDialogView, FilterDialogViewModel>();
        }

        #endregion
    }
}
