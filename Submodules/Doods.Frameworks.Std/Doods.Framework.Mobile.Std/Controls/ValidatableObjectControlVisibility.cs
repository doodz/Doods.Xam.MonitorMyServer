using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Std.controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidatableObjectControlVisibility : ContentView
    {
        public static readonly BindableProperty NextEntryProperty =
            BindableProperty.Create(nameof(NextEntry), typeof(View), typeof(Entry));

        public ValidatableObjectControlVisibility()
        {
            InitializeComponent();
        }

        public View NextEntry
        {
            get => (View) GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        public void OnNext()
        {
            NextEntry?.Focus();
        }
    }
}