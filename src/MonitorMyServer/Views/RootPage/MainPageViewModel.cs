using System.Collections.Generic;
using System.Linq;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
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
            NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage));
        }

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            var viewModelStateItem = new ViewModelStateItem(this);
            viewModelStateItem.IsRunning = false;
            viewModelStateItem.ShowCurrentCmd = _addHostCmd;
            viewModelStateItem.Color = Color.Blue;
            ViewModelStateItem = viewModelStateItem;

        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {

            

            var hosts = await DataProvider.GetHostsAsync();

            if (!hosts.Any()) ShowErrorHostState();

            await Login(hosts);
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            //var viewModelStateItem = new ViewModelStateItem(this);
            //ViewModelStateItem.Title = "";
            //ViewModelStateItem.Description = "";
            if (ViewModelStateItem != null)
            {
                ViewModelStateItem.IsRunning = false;
                ViewModelStateItem.Color = Color.Green;
            }

            // ViewModelStateItem = viewModelStateItem;
        }

        private void ShowErrorHostState()
        {
            var viewModelStateItem = new ViewModelStateItem(this);
            viewModelStateItem.Title = Resource.ErrorNoHostsDetected;
            viewModelStateItem.Description = Resource.ClickAddHost;
            
            viewModelStateItem.IsRunning = true;
            viewModelStateItem.ShowCurrentCmd = _addHostCmd;
            viewModelStateItem.Color = Color.Red;
            ViewModelStateItem = viewModelStateItem;
        }

        private async Task Login(IEnumerable<Host> hosts)
        {

            var host = hosts.First();
            Title = host.HostName;
            var con = new SshConnection(host.Url, host.Port,host.UserName, host.Password);
            _sshService.Init(con, true);
            var toto = new CpuInfoRequest();
           
            var val = await _sshService.ExecuteTaskAsync<string>(toto);
            var total = $"{host.HostName} -- {val.Data}";

            var val2 = await _sshService.ExecuteTaskAsync<CpuInfoBean>(toto);
            total = $"{host.HostName} -- {val.Data}";
        }
    }
}