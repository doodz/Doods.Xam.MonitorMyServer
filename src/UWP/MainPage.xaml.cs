namespace Doods.Xam.MonitorMyServer.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            LoadApplication(new MonitorMyServer.App());
        }
    }
}
