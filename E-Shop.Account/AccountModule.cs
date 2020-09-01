using E_Shop.Account.Dialogs;
using E_Shop.Account.Views;
using E_Shop.Business;
using E_Shop.Core.Interfaces.Repositories;
using E_Shop.Core.Interfaces.Services;
using E_Shop.Infrastructure.Repositories;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E_Shop.Account
{
    public class AccountModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public AccountModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(LoginView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();

            containerRegistry.RegisterForNavigation<LoginView>();

            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
        }
    }
}
