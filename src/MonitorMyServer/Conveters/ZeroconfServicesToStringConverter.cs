using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.Conveters
{
    public class ZeroconfServicesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IReadOnlyDictionary<string, IService> lst) return Convert(lst.Values);


            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        private string Convert(IEnumerable<IService> lst)
        {
            var convert = string.Join(" - ", lst.Select(l => l.Name));


            convert = $"{lst.Count()} : {convert}";
            return convert;
        }
    }
}