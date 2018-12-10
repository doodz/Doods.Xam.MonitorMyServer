using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views
{
    public class MainPageViewModel : ViewModelWhithState
    {
        private readonly ICommand _addHostCmd;
        private readonly ISshService _sshService;
        private CpuInfo _cpuInfo;

        private IEnumerable<DiskUsage> _disksUsage;

        private int _upgradablesCount;


        public int UpgradablesCount
        {
            get => _upgradablesCount;
            set => SetProperty(ref _upgradablesCount, value);
        }

        private IEnumerable<Upgradable> _upgradables;


        public MainPageViewModel(ISshService sshService)
        {
            Title = Resource.Home;
            _sshService = sshService;
            CmdState = RefreshCmd;
            _addHostCmd = new Command(
                AddHost);
            SetCommandForStateView(_addHostCmd);
        }

        public CpuInfo CpuInfo
        {
            get => _cpuInfo;
            set => SetProperty(ref _cpuInfo, value);
        }

        public IEnumerable<DiskUsage> DisksUsage
        {
            get => _disksUsage;
            set => SetProperty(ref _disksUsage, value);
        }

        public IEnumerable<Upgradable> Upgradables
        {
            get => _upgradables;
            set => SetProperty(ref _upgradables, value);
        }

        private void AddHost()
        {
            //NavigationService.NavigateAsync(nameof(HostManagerPage));
            NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage));
        }


        protected override void OnInitializeLoading(LoadingContext context)
        {
            //viewModelStateItem.ShowCurrentCmd = _addHostCmd;
            //viewModelStateItem.Color = Color.Blue;
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            var hosts = await DataProvider.GetHostsAsync();
            if (!hosts.Any()) ShowErrorHostState();
            await Login(hosts);
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            if (ViewModelStateItem != null)
            {
                ViewModelStateItem.IsRunning = false;
                ViewModelStateItem.Color = Color.Green;
            }
        }

        private void ShowErrorHostState()
        {
            SetLabelsStateItem(Resource.ErrorNoHostsDetected, Resource.ClickAddHost);


            //viewModelStateItem.ShowCurrentCmd = _addHostCmd;
            //viewModelStateItem.Color = Color.Red;
        }

        private async Task Login(IEnumerable<Host> hosts)
        {
            var host = hosts.First();
            Title = host.HostName;
            var con = new SshConnection(host.Url, host.Port, host.UserName, host.Password);
            _sshService.Init(con, true);

            SetLabelsStateItem(host.HostName, host.Url);


            //var result1 = await _sshService.ExecuteTaskAsync<DiskUsageBeanWhapper>(diskUsageRequest);
            var taskCpuInfo = GetCpuInfo();
            var taskDisk = GetDisksUsage();
            var taskUpgradables= GetUpgradables();

            await Task.WhenAll(taskCpuInfo, taskDisk, taskUpgradables);
        }

        private async Task GetUpgradables()
        {
            var upgradableRequest = new UpgradableRequest();
            var upgradableBean = await _sshService.ExecuteTaskAsync<IEnumerable<UpgradableBean>>(upgradableRequest);
            Upgradables = Mapper.Map<IEnumerable<UpgradableBean>, IEnumerable<Upgradable>>(upgradableBean.Data);
            UpgradablesCount = _upgradables.Count();
        }

        private async Task GetCpuInfo()
        {
            var cpuInfoRequest = new CpuInfoRequest();
            var cpuInfoBean = await _sshService.ExecuteTaskAsync<CpuInfoBean>(cpuInfoRequest);
            CpuInfo = Mapper.Map<CpuInfoBean, CpuInfo>(cpuInfoBean.Data);
        }


        private async Task GetDisksUsage()
        {
            var diskUsageRequest = new DiskUsageRequest();
            var diskUsageBean = await _sshService.ExecuteTaskAsync<IEnumerable<DiskUsageBean>>(diskUsageRequest);
            DisksUsage = Mapper.Map<IEnumerable<DiskUsageBean>, IEnumerable<DiskUsage>>(diskUsageBean.Data);
        }
    }
}