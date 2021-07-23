using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Converters
{
    public class ImageEnumEmbeddedResourceConverter : TypeConverter, IValueConverter
    {
        /// <summary>
        ///     Convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
                return null;


            return ConvertFromInvariantString(str);
        }

        /// <summary>
        ///     ConvertBack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ConvertFromInvariantString(string resourceName)
        {
            //var MainAssembly = Application.Current?.GetType()?.GetTypeAssemblyFullName();
            var assemblyName = typeof(ImageEnumEmbeddedResourceConverter).Assembly;

            var assemblyFullName = assemblyName.GetName().Name;
            return $"resource://{assemblyFullName}.Resources.Svg.{resourceName}?assembly={assemblyFullName}";
        }
    }
}