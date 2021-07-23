using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}