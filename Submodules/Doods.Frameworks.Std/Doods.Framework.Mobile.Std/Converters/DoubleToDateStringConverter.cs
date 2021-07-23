using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class DoubleToDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double)) return value;

            var timeSpan = TimeSpan.FromSeconds((double) value);

            if (parameter is string str) return timeSpan.ToString(str);

            return timeSpan.ToString(string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}