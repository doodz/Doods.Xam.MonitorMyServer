using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views
{
    public class MainPageViewModel : ViewModel
    {

        private readonly ICommand _addHostCmd;
        private readonly ISshService _sshService;
        private ViewModelStateItem _viewModelStateItem;

        public MainPageViewModel(ISshService sshService)
        {
            Title = Resource.Home;
            _sshService = sshService;
            CmdState = RefreshCmd;
            _addHostCmd = new Command(
                AddHost);
        }

        private void AddHost()
        {
            //NavigationService.NavigateAsync(nameof(HostManagerPage));
            NavigationService.NavigateAsync(nameof(LogInPage));
        }

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            //var viewModelStateItem = new ViewModelStateItem(this);
            //viewModelStateItem.IsRunning = false;
            //viewModelStateItem.Color =Color.Blue;
            //ViewModelStateItem = viewModelStateItem;

        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            var count = await DataProvider.CountHostAsync();

            if (count <= 0) ShowErrorHostState();

            await Login(count);
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            //var viewModelStateItem = new ViewModelStateItem(this);
            //ViewModelStateItem.Title = "";
            //ViewModelStateItem.Description = "";
            ViewModelStateItem.IsRunning = false;
            ViewModelStateItem.Color = Color.Green;
           // ViewModelStateItem = viewModelStateItem;
        }

        private void ShowErrorHostState()
        {
            var viewModelStateItem = new ViewModelStateItem(this);
            viewModelStateItem.Title = "Error : no hosts detected";
            viewModelStateItem.Description = "Click to add host";
            
            viewModelStateItem.IsRunning = true;
            viewModelStateItem.ShowCurrentCmd = _addHostCmd;
            viewModelStateItem.Color = Color.Red;
            ViewModelStateItem = viewModelStateItem;
        }

        private async Task Login(int count)
        {

            await Task.Delay(1000);
            var con = new SshConnection("192.168.1.73", "pi", "raspberry");
            _sshService.Init(con, true);
            var toto = new toto();
            toto.Handler = toto;
            var val = await _sshService.ExecuteTaskAsync<string>(toto);
            var total = $"{count} -- {val.Data}";
        }
    }
}