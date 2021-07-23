using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var r = value == null;
            if (parameter?.ToString() == "!")
                r = !r;

            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}