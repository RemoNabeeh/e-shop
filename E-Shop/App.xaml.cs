using E_Shop.Account;
using E_Shop.Cart;
using E_Shop.Catalog;
using E_Shop.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace E_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AccountModule>();
            moduleCatalog.AddModule<CatalogModule>();
            moduleCatalog.AddModule<CartModule>();
        }
    }
}
