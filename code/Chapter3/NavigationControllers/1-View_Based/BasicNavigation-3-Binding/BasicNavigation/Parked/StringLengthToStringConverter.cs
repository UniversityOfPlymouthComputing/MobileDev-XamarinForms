using System;
using System.Globalization;
using Xamarin.Forms;

namespace BasicNavigation
{
    public class StringLengthToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return "Enter a Name";
            } else
            {
                return "Enter a Name and Click Save";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
