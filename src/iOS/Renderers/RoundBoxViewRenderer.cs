using CoreGraphics;
using Doods.Framework.Mobile.Std.controls;
using Doods.Xam.MonitorMyServer.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(RoundBoxView), typeof(RoundBoxViewRenderer))]
namespace Doods.Xam.MonitorMyServer.iOS.Renderers
{
    public class RoundBoxViewRenderer : BoxRenderer
    {
        public override void Draw(CGRect frame)
        {
            var box = Element as RoundBoxView;
            if (box == null)
                return;

            var circleDotFillPath = UIBezierPath.FromOval(new CGRect(frame.GetMinX(), frame.GetMinY(), frame.Width, frame.Width));
            var colorFillColor = box.BackgroundColor.ToUIColor();
            colorFillColor.SetFill();
            circleDotFillPath.Fill();

            if (box.BorderThickness <= 0)
                return;

            var circleDotStrokePath = UIBezierPath.FromOval(new CGRect(frame.GetMinX() + 1.0f, frame.GetMinY() + 1.0f, frame.Width - 2.0f, frame.Height - 2.0f));
            box.BorderColor.ToUIColor().SetStroke();
            circleDotStrokePath.Stroke();
        }
    }
}