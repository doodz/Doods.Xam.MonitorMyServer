using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class TitleSwitchTemplate : TitleTemplate
    {
        public Switch Switch;

        public TitleSwitchTemplate()
        {
            Switch = new Switch();
            Switch.SetBinding(Switch.IsToggledProperty, new TemplateBinding(nameof(TitleSwitchView.IsToggled)));
            //TitleLabel.TextColor = default;
            //SubTitleLabel.TextColor = default;

            var grid = new Grid();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Command = new Command(() => Switch.IsToggled = !Switch.IsToggled);
            grid.GestureRecognizers.Add(tapGestureRecognizer);


            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});

            grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            grid.Children.Add(TitleLabel, 0, 0);
            grid.Children.Add(SubTitleLabel, 0, 1);
            grid.Children.Add(Switch, 1, 0);
            Grid.SetRowSpan(Switch, 2);
            var content = new ContentPresenter();
            grid.Children.Add(content, 0, 2);
            Grid.SetColumnSpan(Switch, 2);
            Content = grid;
        }
    }
}