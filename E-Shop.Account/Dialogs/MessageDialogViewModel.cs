using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace E_Shop.Account.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;

        public string Title => "Login";

        public event Action<IDialogResult> RequestClose;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public DelegateCommand CloseDialogCommand { get; private set; }
        
        public MessageDialogViewModel()
        {
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
