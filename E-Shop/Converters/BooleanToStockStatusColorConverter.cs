using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace E_Shop.Converters
{
    public class BooleanToStockStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.ForestGreen : Brushes.OrangeRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
