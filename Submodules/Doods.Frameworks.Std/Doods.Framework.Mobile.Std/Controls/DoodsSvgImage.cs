using System.Collections.Generic;
using Doods.Framework.Mobile.Std.Enum;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class DoodsSvgImage : SvgCachedImage
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color),
            typeof(Color),
            typeof(DoodsSvgImage), propertyChanged: ColorPropertyChanged);

        public DoodsSvgImage()
        {
            //Xamarin.Forms.Color redButtonStyle = (Xamarin.Forms.Color)Resources["WindowBackgroundColor"];

            ReplaceStringMap = SvgIconTarget.ReplaceColor;
        }

        //SystemRed
        //SystemGreen
        //WindowBackgroundColor
        //todo add statut => error ok enable ...
        public static Dictionary<string, string> ReplaceColorToRed => new Dictionary<string, string>() {{"#ff0000", "#FF3B30"}};
        public static Dictionary<string, string> ReplaceColorToGreen => new Dictionary<string, string>() {{"#ff0000", "#34C759"}};

        public static Dictionary<string, string> ReplaceColorToBlack => new Dictionary<string, string>() {{"#ff0000", "#000000"}};


        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }


        private static void ColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (DoodsSvgImage) bindable;

            if (newvalue is Color c)
            {
                control.ReplaceStringMap = new Dictionary<string, string>
                    {{"#ff0000", c.ToHex()}, {"#000000", c.ToHex()}};
                ;
                //control.ReloadImage();
            }
        }
    }
}