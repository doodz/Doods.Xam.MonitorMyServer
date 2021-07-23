using System;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    /// <summary>
    ///     Control template for Title only.
    /// </summary>
    public class TitleTemplate : ContentView
    {
        public readonly Label SubTitleLabel;

        public readonly Label TitleLabel;

        public TitleTemplate()
        {
            TitleLabel = new Label();
            TitleLabel.SetBinding(Label.TextProperty, new TemplateBinding(nameof(TitledFrameView.Title)));
            TitleLabel.SetBinding(StyleProperty, new TemplateBinding(nameof(TitledFrameView.TitleStyle)));
            TitleLabel.HorizontalOptions = LayoutOptions.FillAndExpand;

            SubTitleLabel = new Label();
            SubTitleLabel.SetBinding(Label.TextProperty, new TemplateBinding(nameof(TitledFrameView.SubTitle)));
            SubTitleLabel.SetBinding(StyleProperty, new TemplateBinding(nameof(TitledFrameView.SubTitleStyle)));
            SubTitleLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            SubTitleLabel.HorizontalTextAlignment = TextAlignment.End;

            var frame = new Frame
            {
                Margin = 0,
                Padding = 0,
                CornerRadius = 8,
                Content = new StackLayout
                {
                    Children =
                    {
                        new StackLayout
                        {
                            Padding = 4,
                            BackgroundColor = Color.CornflowerBlue,
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 5,
                            Children = {TitleLabel, SubTitleLabel}
                        },
                        new ContentPresenter()
                    }
                }
            };
            Content = frame;


            BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
        }
    }
}