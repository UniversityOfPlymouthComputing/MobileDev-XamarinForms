using System;
using System.Globalization;
using Xamarin.Forms;

namespace uoplib.converters
{
    public class BoolToSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? ListViewSelectionMode.Single : ListViewSelectionMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ListViewSelectionMode)value == ListViewSelectionMode.Single);
        }
    }
}
