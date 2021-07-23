using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class TimeSpanToDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan timeSpan)) return value;


            if (parameter is string str) return timeSpan.ToString(str);

            return timeSpan.ToString(string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class TimeSpanToDateHumanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan timeSpan)) return value;


            if (parameter is string str) return string.Format(str, timeSpan);

            return timeSpan.ToString(string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}