using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Comtrols
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {

        public FlyoutHeader()
        {
            InitializeComponent();
            Banner.Source = "MMS_graphic.png";
        }

        public FlyoutHeader(string banner)
        {
            InitializeComponent();
            Banner.Source = banner;
        }
    }
}