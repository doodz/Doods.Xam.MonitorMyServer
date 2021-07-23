using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.controls
{
    public class RoundBoxView : BoxView
    {
        /// <summary>
        ///     Thickness property of border
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(RoundBoxView), 0);


        /// <summary>
        ///     Color property of border
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundBoxView), Color.White);

        /// <summary>
        ///     Border thickness of circle image
        /// </summary>
        public int BorderThickness
        {
            get => (int) GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        ///     Border Color of circle image
        /// </summary>
        public Color BorderColor
        {
            get => (Color) GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}