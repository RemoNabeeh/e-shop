using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace E_Shop.Dialogs
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Filter Products";

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

        public DelegateCommand ApplyFilterCommand { get; set; }

        public FilterDialogViewModel()
        {
            ApplyFilterCommand = new DelegateCommand(ApplyFilter);
        }

        
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

        private void ApplyFilter()
        {
            var dialogParams = new DialogParameters();
            dialogParams.Add("MinValue", MinValue);
            dialogParams.Add("MaxValue", MaxValue);

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParams));
        }
    }
}
