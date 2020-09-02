using Prism.Mvvm;

namespace E_Shop.Models
{
    public class FilterModel : BindableBase
    {
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

        private int currentMinValue;
        public int CurrentMinValue
        {
            get { return currentMinValue; }
            set { SetProperty(ref currentMinValue, value); }
        }

        private int currentMaxValue;
        public int CurrentMaxValue
        {
            get { return currentMaxValue; }
            set { SetProperty(ref currentMaxValue, value); }
        }
    }
}
