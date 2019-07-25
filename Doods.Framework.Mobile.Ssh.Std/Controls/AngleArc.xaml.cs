using System;
using Doods.Framework.Mobile.Std.controls;
using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Ssh.Std.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AngleArc : ContentView
    {
        public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(nameof(SweepAngle),
            typeof(float),
            typeof(AngleArc), propertyChanged: PropertyChanged);
        /// <summary>
        /// 
        /// </summary>
        private readonly SKPaint _arcPaintGreen = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 15,
            Color = Color.LightSeaGreen.ToSKColor()
        };

        /// <summary>
        /// 
        /// </summary>
        private readonly SKPaint _arcPaintRed = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 15,
            Color = Color.LightCoral.ToSKColor()
        };


        /// <summary>
        /// 
        /// </summary>
        private readonly SKPaint _outlinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 15,
            Color = Color.LightGray.ToSKColor()
        };

        public AngleArc()
        {
            InitializeComponent();
        }

        public float SweepAngle
        {
            get => (float) GetValue(SweepAngleProperty);
            set => SetValue(SweepAngleProperty, value);
        }

        private static void PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is AngleArc toto) toto.SliderValueChanged(null, null);
        }

        private void SliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            canvasView?.InvalidateSurface();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var val = 60;

            var rect = new SKRect(val, val, info.Width - val, info.Height - val);
            var startAngle = -0f;
            var sweepAnglelocal = SweepAngle * 3.60f;
            var flour = (float)Math.Floor(sweepAnglelocal);
            var arcPaint = SweepAngle > 80 ? _arcPaintRed : _arcPaintGreen;

            //canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, _outlinePaint);
            //using (var path = new SKPath())
            //{
            //    path.AddCircle(0, startAngle, 100);
            //   path.ad
            //    canvas.DrawPath(path, arcPaint);
            //}


            canvas.DrawOval(rect, _outlinePaint);

            using (var path = new SKPath())
            {
                path.AddArc(rect, startAngle, flour);
                canvas.DrawPath(path, arcPaint);
            }
        }

        //void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        //{
        //    SKImageInfo info = args.Info;
        //    SKSurface surface = args.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();

        //    SKPaint paint = new SKPaint
        //    {
        //        Style = SKPaintStyle.Stroke,
        //        Color = Color.Red.ToSKColor(),
        //        StrokeWidth = SweepAngle
        //    };
        //    canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

        //    paint.Style = SKPaintStyle.Fill;
        //    paint.Color = SKColors.Blue;
        //    canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
        //}
    }
}