using Prism.Mvvm;

namespace E_Shop.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private string _title = "E-Shop Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
