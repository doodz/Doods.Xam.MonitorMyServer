using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Ssh.Std.Controls
{
    /// <summary>
    ///     http://www.e-naxos.com/Blog/post/Les-papiers-des-lecteurs-Creer-un-controle-graphique-Xamarin-avec-SkiaSharp.aspx
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gauge : ContentView
    {
        public static readonly BindableProperty ValeurProperty =
            BindableProperty.Create(nameof(Valeur),
                typeof(string),
                typeof(Gauge),
                "0.0",
                BindingMode.Default,
                propertyChanged: UpdateValeur);

        private float valeur;

        public Gauge()
        {
            InitializeComponent();
        }


        public string Valeur
        {
            get => (string) GetValue(ValeurProperty);
            set => SetValue(ValeurProperty, value);
        }

        private static void UpdateValeur(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (Gauge) bindable;
            var v = (string) newValue;
            ctrl.valeur = float.Parse(v.Replace(".", ","));
            ctrl.canvasView.InvalidateSurface();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var width = e.Info.Width;
            var height = e.Info.Height;

            canvas.Translate(width / 2, height / 2);

            var Rect = new SKRect(-100, -100, 100, 100);


            var scale = Math.Min(width / Rect.Width, height / Rect.Height);

            canvas.Scale(scale);

            using (var path = new SKPath())

            {
                path.ArcTo(new SKRect(-87.5f, -87.5f, 87.5f, 87.5f), 135, 270, false);

                canvas.DrawPath(path, new SKPaint
                {
                    Color = Color.Gray.ToSKColor(),
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 25
                });
            }


            var AngleValeur = 270 * valeur / 100;
            using (var path = new SKPath())
            {
                path.ArcTo(new SKRect(-87.5f, -87.5f, 87.5f, 87.5f), 135, AngleValeur, false);
                canvas.DrawPath(path, new SKPaint
                {
                    Color = Color.CornflowerBlue.ToSKColor(),
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 25
                });
            }

            canvas.DrawText(valeur.ToString("0.0") + "%", 0, 10, new SKPaint
            {
                Color = Color.CornflowerBlue.ToSKColor(),
                Style = SKPaintStyle.Fill,
                StrokeWidth = 1,
                TextAlign = SKTextAlign.Center,
                TextSize = 40
            });
        }
    }
}