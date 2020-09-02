using E_Shop.Core.Interfaces.Services;
using E_Shop.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace E_Shop.Dialogs.ViewModels
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IStringsResourceService _stringsResourceService;

        public string Title => _stringsResourceService.GetString(Constants.LoginDialogTitle);

        public event Action<IDialogResult> RequestClose;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public DelegateCommand CloseDialogCommand { get; private set; }
        
        public MessageDialogViewModel(IStringsResourceService stringsResourceService)
        {
            _stringsResourceService = stringsResourceService;

            CloseDialogCommand = new DelegateCommand(OnDialogClosed);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult());
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}
