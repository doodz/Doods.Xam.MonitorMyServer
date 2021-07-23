using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.controls
{
    public class AspectRatioConverter : ContentView
    {
        public readonly BindableProperty AspectRatioProperty =
            BindableProperty.Create(nameof(AspectRatio), typeof(double), typeof(AspectRatioConverter), 1d);


        public double AspectRatio
        {
            get => (double) GetValue(AspectRatioProperty);
            set => SetValue(AspectRatioProperty, value);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, widthConstraint * AspectRatio));
        }
    }
}