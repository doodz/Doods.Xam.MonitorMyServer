using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostTemplate : ViewCell
    {
        public static readonly BindableProperty CommandActionProperty =
            BindableProperty.Create(nameof(CommandAction), typeof(ICommand), typeof(HostTemplate), true);

        public HostTemplate()
        {
            InitializeComponent();
        }

        public ICommand CommandAction
        {
            get => (ICommand) GetValue(CommandActionProperty);
            set => SetValue(CommandActionProperty, value);
        }


        protected override void OnBindingContextChanged()
        {
            var toto = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}