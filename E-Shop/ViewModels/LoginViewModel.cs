using E_Shop.Dialogs;
using E_Shop.Core.Dialogs;
using E_Shop.Core.Interfaces.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Text.RegularExpressions;
using E_Shop.Enums;
using E_Shop.Views;

namespace E_Shop.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        #region [Properties]

        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        #endregion

        #region [Commands]

        public DelegateCommand LoginCommand { get; private set; }

        #endregion

        #region [Ctor]

        public LoginViewModel(IRegionManager regionManager, IUserService userService, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _userService = userService;
            _dialogService = dialogService;
            
            LoginCommand = new DelegateCommand(Login, CanLogin).ObservesProperty(() => Username);
        }

        #endregion

        #region [Methods]

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username);
        }

        private void Login()
        {
            if (!ValidateUsername())
            {
                ShowDialog("Invalid username.\n\nUsername must not contain spaces or any special characters.");
            }
            else
            {
                var isExist = _userService.IsUserExists(Username);
                if (!isExist)
                {
                    ShowDialog("Username is not exist.");
                }
                else
                {
                    NavigateToHome();
                }
            }
        }

        private bool ValidateUsername()
        {
            Regex regex = new Regex(@"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$");
            Match match = regex.Match(Username);
            return !(match == null || match == Match.Empty);
        }

        private void ShowDialog(string message)
        {
            _dialogService.ShowMessageDialog(nameof(MessageDialogView), message, null);
        }

        private void NavigateToHome()
        {
            var param = new NavigationParameters
            {
                { "username", Username }
            };

            _regionManager.RequestNavigate(RegionNames.WindowRegion.ToString(), nameof(HomeView), param);
            _regionManager.RequestNavigate(RegionNames.ContentRegion.ToString(), nameof(CatalogView), param);
        }

        #endregion

        #region [Navigation]

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Username = string.Empty;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Username = string.Empty;
        }

        #endregion
    }
}
