using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls.Card
{
    public class DateTimeView : ContentView
    {
        public DateTimeView(Card card)
        {
            var labelStyle = new Style(typeof(Label))
                .Set(Label.FontSizeProperty, 8)
                .Set(Label.TextColorProperty, StyleKit.MediumGrey)
                .Set(VerticalOptionsProperty, LayoutOptions.Center);

            var iconStyle = new Style(typeof(Image))
                .Set(HeightRequestProperty, 10)
                .Set(WidthRequestProperty, 10)
                .Set(VerticalOptionsProperty, LayoutOptions.Center);

            var stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 20,
                Padding = new Thickness(0),
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Image
                    {
                        Style = iconStyle,
                        Source = StyleKit.Icons.SmallCalendar
                    },
                    new Label
                    {
                        Text = card.DueDate.ToShortDateString(),
                        Style = labelStyle
                    },
                    new BoxView {Color = Color.Transparent, WidthRequest = 20},
                    new Image
                    {
                        Style = iconStyle,
                        Source = StyleKit.Icons.SmallClock
                    },
                    new Label
                    {
                        Text = card.DirationInMinutes + " Minutes",
                        Style = labelStyle
                    }
                }
            };

            Content = stack;
        }
    }
}