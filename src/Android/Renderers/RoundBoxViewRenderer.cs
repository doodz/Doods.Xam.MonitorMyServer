using Android.Content;
using Android.Graphics;
using Doods.Framework.Mobile.Std.controls;
using Doods.Xam.MonitorMyServer.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundBoxView), typeof(RoundBoxViewRenderer))]
namespace Doods.Xam.MonitorMyServer.Droid.Renderers
{
    public class RoundBoxViewRenderer : BoxRenderer
    {
        public RoundBoxViewRenderer(Context c) : base(c)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            SetWillNotDraw(false);
            Invalidate();
        }

        public override void Draw(Canvas canvas)
        {
            var box = Element as RoundBoxView;
            var rect = new Android.Graphics.Rect();
            var color = box.BorderColor != Xamarin.Forms.Color.White ? box.BorderColor.ToAndroid() : box.BackgroundColor.ToAndroid();
            var paint = new Paint
            {
                Color = color,
                AntiAlias = true
            };
            GetDrawingRect(rect);
            var radius = (float)(rect.Width() / 14 * 8); // ... / box width * box corner radius
            canvas.DrawRoundRect(new RectF(rect), radius, radius, paint);

            if (box.BorderThickness <= 0)
                return;

            var diameter = rect.Width() - box.BorderThickness;
            var rect1 = new Android.Graphics.Rect(box.BorderThickness, box.BorderThickness, diameter, diameter);
            var paint1 = new Paint
            {
                Color = box.BackgroundColor.ToAndroid(),
                AntiAlias = true
            };
            canvas.DrawRoundRect(new RectF(rect1), radius, radius, paint1);
        }
    }

}