using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls.Card
{
    internal static class StyleExtension
    {
        public static Style Clone(this Style _style)
        {
            var newStyle = new Style(_style.TargetType)
            {
                BasedOn = _style
            };
            return newStyle;
        }

        public static Style Set<T>(this Style _style, BindableProperty property, T value)
        {
            _style.Setters.Add(new Setter {Property = property, Value = value});
            return _style;
        }
    }
}