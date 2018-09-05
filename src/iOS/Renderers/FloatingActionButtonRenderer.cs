using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Xam.MonitorMyServer.iOS.Helpers;
using Doods.Xam.MonitorMyServer.iOS.Renderers;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace Doods.Xam.MonitorMyServer.iOS.Renderers
{
    public class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButton, MNFloatingActionButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var fab = new MNFloatingActionButton(Element.AnimateOnSelection)
                {
                    Frame = new CoreGraphics.CGRect(0, 0, 24, 24)
                };

                SetNativeControl(fab);

                UpdateStyle();
            }

            if (e.NewElement != null)
            {
                Control.TouchUpInside += Fab_TouchUpInside;
            }

            if (e.OldElement != null)
            {
                Control.TouchUpInside -= Fab_TouchUpInside;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FloatingActionButton.SizeProperty.PropertyName)
            {
                SetSize();
            }
            else if (e.PropertyName == FloatingActionButton.NormalColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.RippleColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.DisabledColorProperty.PropertyName)
            {
                SetBackgroundColors();
            }
            else if (e.PropertyName == FloatingActionButton.HasShadowProperty.PropertyName)
            {
                SetHasShadow();
            }
            else if (e.PropertyName == FloatingActionButton.SourceProperty.PropertyName)
            {
                SetImage();
            }
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
            {
                UpdateEnabled();
            }
            else if (e.PropertyName == FloatingActionButton.AnimateOnSelectionProperty.PropertyName)
            {
                UpdateAnimateOnSelection();
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var viewSize = this.Element.Size == FloatingActionButtonSize.Normal ? 56 : 40;

            return new SizeRequest(new Size(viewSize, viewSize));
        }

        private void UpdateStyle()
        {
            SetSize();

            SetBackgroundColors();

            SetHasShadow();

            SetImage();

            UpdateEnabled();
        }

        private void SetSize()
        {
            switch (this.Element.Size)
            {
                case FloatingActionButtonSize.Mini:
                    this.Control.Size = MNFloatingActionButton.FabSize.Mini;
                    break;
                case FloatingActionButtonSize.Normal:
                    this.Control.Size = MNFloatingActionButton.FabSize.Normal;
                    break;
            }

            //Control.Size = MNFloatingActionButton.FabSize.Normal;
        }

        private void SetBackgroundColors()
        {
            Control.BackgroundColor = Element.NormalColor.ToUIColor();
            //this.Control.PressedBackgroundColor = this.Element.Ripplecolor.ToUIColor();
        }

        private void SetHasShadow()
        {
            //Control.HasShadow = false;

        }

        private void SetImage()
        {
            SetImageAsync(Element.Source, Control);
        }

        private void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;

            if (Control.Enabled == false)
                Control.BackgroundColor = Element.DisabledColor.ToUIColor();
            else
            {
                SetBackgroundColors();
            }
        }

        private void UpdateAnimateOnSelection()
        {
            Control.AnimateOnSelection = Element.AnimateOnSelection;
        }

        private void Fab_TouchUpInside(object sender, EventArgs e)
        {
            Element.SendClicked();
        }

        private static async void SetImageAsync(ImageSource source, MNFloatingActionButton targetButton)
        {
            if (source != null)
            {
                var widthRequest = targetButton.Frame.Width;
                var heightRequest = targetButton.Frame.Height;

                var handler = GetHandler(source);
                using (var image = await handler.LoadImageAsync(source))
                {
                    if (image != null)
                    {
                        UIGraphics.BeginImageContextWithOptions(new CoreGraphics.CGSize(widthRequest, heightRequest), false, UIScreen.MainScreen.Scale);
                        image.Draw(new CoreGraphics.CGRect(0, 0, widthRequest, heightRequest));
                        using (var resultImage = UIGraphics.GetImageFromCurrentImageContext())
                        {
                            if (resultImage != null)
                            {
                                UIGraphics.EndImageContext();
                                using (var resizableImage = resultImage.CreateResizableImage(new UIEdgeInsets(0f, 0f, widthRequest, heightRequest)))
                                {
                                    targetButton.CenterImageView.Image = resizableImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                                }
                            }
                        }
                    }
                    else
                    {
                        targetButton.CenterImageView.Image = null;
                    }
                }
            }
            else
            {
                targetButton.CenterImageView.Image = null;
            }
        }

        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }
    }
}