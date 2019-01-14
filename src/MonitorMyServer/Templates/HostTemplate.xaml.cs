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


        protected override void OnBindingContextChanged()
        {
            var toto = BindingContext;
            base.OnBindingContextChanged();
        }

        public ICommand CommandAction
        {
            get => (ICommand) GetValue(CommandActionProperty);
            set => SetValue(CommandActionProperty, value);
        }
    }
}