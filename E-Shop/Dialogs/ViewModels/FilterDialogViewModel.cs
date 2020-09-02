using E_Shop.Core.Interfaces.Services;
using E_Shop.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace E_Shop.Dialogs.ViewModels
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IStringsResourceService _stringsResourceService;

        public string Title => _stringsResourceService.GetString(Constants.FilterDialogTitle);

        public event Action<IDialogResult> RequestClose;

        private int _minValue;
        public int MinValue
        {
            get { return _minValue; }
            set { SetProperty(ref _minValue, value); }
        }

        private int _maxValue;
        public int MaxValue
        {
            get { return _maxValue; }
            set { SetProperty(ref _maxValue, value); }
        }

        public DelegateCommand<string> CloseDialogCommand { get; set; }

        #region [Ctor]

        public FilterDialogViewModel(IStringsResourceService stringsResourceService)
        {
            _stringsResourceService = stringsResourceService;

            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        #endregion

        #region [Dialog Methods]

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion

        private void CloseDialog(string parameter)
        {
            ButtonResult result;

            if (parameter?.ToLower() == Constants.FilterDialogButton.ToLower())
            {
                result = ButtonResult.OK;

                var dialogParams = new DialogParameters
                {
                    { Constants.FilterMinValue, MinValue },
                    { Constants.FilterMaxnValue, MaxValue }
                };

                RaiseRequestClose(new DialogResult(result, dialogParams));
            }
            else if (parameter?.ToLower() == Constants.CancelDialogButton.ToLower())
            {
                result = ButtonResult.Cancel;

                RaiseRequestClose(new DialogResult(result));
            }
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}
