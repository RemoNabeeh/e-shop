using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace E_Shop.App.Dialogs
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        public string Title { get; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
