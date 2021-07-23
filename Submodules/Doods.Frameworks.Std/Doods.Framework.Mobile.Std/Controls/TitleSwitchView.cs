using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class TitleSwitchView : TitledFrameView
    {
        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled),
            typeof(bool), typeof(TitleSwitchView), false, BindingMode.TwoWay);

        private readonly ControlTemplate _titleTemplate = new ControlTemplate(typeof(TitleSwitchTemplate));

        public TitleSwitchView()
        {
            ControlTemplate = _titleTemplate;
            var tapGestureRecognizer = new TapGestureRecognizer();
        }

        public bool IsToggled
        {
            get => (bool) GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }
    }
}