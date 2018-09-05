using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Xam.MonitorMyServer.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace Doods.Xam.MonitorMyServer.Droid.Renderers
{
    public class FloatingActionButtonRenderer :
       ViewRenderer<FloatingActionButton, Android.Support.Design.Widget.FloatingActionButton>
    {
        public FloatingActionButtonRenderer(Context c) : base(c)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                ViewGroup.SetClipChildren(false);
                ViewGroup.SetClipToPadding(false);
                UpdateControlForSize();

                UpdateStyle();
            }

            if (e.NewElement != null)
                Control.Click += Fab_Click;
            else if (e.OldElement != null)
                Control.Click -= Fab_Click;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FloatingActionButton.SizeProperty.PropertyName)
                UpdateControlForSize();
            else if (e.PropertyName == FloatingActionButton.NormalColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.RippleColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.DisabledColorProperty.PropertyName)
                SetBackgroundColors();
            else if (e.PropertyName == FloatingActionButton.HasShadowProperty.PropertyName)
                SetHasShadow();
            else if (e.PropertyName == FloatingActionButton.SourceProperty.PropertyName)
                SetImage();
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
            else
                base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Control.Click -= Fab_Click;

            base.Dispose(disposing);
        }

        private void UpdateControlForSize()
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);

            Android.Support.Design.Widget.FloatingActionButton fab = null;

            if (Element.Size == FloatingActionButtonSize.Mini)
                fab =
                    (Android.Support.Design.Widget.FloatingActionButton)
                    inflater.Inflate(Resource.Layout.mini_fab, null);
            else // then normal
                fab =
                    (Android.Support.Design.Widget.FloatingActionButton)
                    inflater.Inflate(Resource.Layout.normal_fab, null);

            SetNativeControl(fab);
            UpdateStyle();
        }

        private void UpdateStyle()
        {
            SetBackgroundColors();

            SetHasShadow();

            SetImage();

            UpdateEnabled();
        }

        private void SetBackgroundColors()
        {
            Control.BackgroundTintList = ColorStateList.ValueOf(Element.NormalColor.ToAndroid());
        }

        private void SetHasShadow()
        {
            return;

            try
            {
                if (Element.HasShadow)
                    ViewCompat.SetElevation(Control, 20);
                else
                    ViewCompat.SetElevation(Control, 0);
            }
            catch
            {
            }

            Control.Elevation = 0f;
            Control.TranslationZ = 0f;
        }

        private void SetImage()
        {
            Task.Run(async () =>
            {
                var bitmap = await GetBitmapAsync(Element.Source);
                if (bitmap != null)
                {
                    (Context as Activity).RunOnUiThread(() => { Control?.SetImageBitmap(bitmap); });
                }
            }).ConfigureAwait(false);
        }

        private void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;

            if (Control.Enabled == false)
                Control.BackgroundTintList = ColorStateList.ValueOf(Element.DisabledColor.ToAndroid());
            else
                UpdateBackgroundColor();
        }

        private async Task<Bitmap> GetBitmapAsync(ImageSource source)
        {
            var handler = GetHandler(source);
            Bitmap returnValue = null;

            try
            {
                returnValue = await handler.LoadImageAsync(source, Context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return returnValue;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Element.SendClicked();
        }

        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
                returnValue = new ImageLoaderSourceHandler();
            else if (source is FileImageSource)
                returnValue = new FileImageSourceHandler();
            else if (source is StreamImageSource)
                returnValue = new StreamImagesourceHandler();
            return returnValue;
        }
    }
}