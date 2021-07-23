using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class ByteCountToHumanReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (long.TryParse(value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var r))
            {
                var param = true;
                if (parameter is bool)
                    param = (bool) parameter;

                return HumanReadableByteCount(r, param);
            }

            return 0L;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string HumanReadableByteCount(long bytes, bool si)
        {
            var unit = si ? 1000 : 1024;
            if (bytes < unit)
                return bytes + " B";
            var exp = (int) (Math.Log(bytes) / Math.Log(unit));
            var pre = (si ? "kMGTPE" : "KMGTPE").ElementAt(exp - 1)
                      + (si ? "" : "i");
            return $"{bytes / Math.Pow(unit, exp)} {pre}";
        }
    }
}