using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Processes2;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views
{
    public class MainPageViewModel : ViewModelWhithState
    {
        private readonly ICommand _addHostCmd;
       
        private readonly IMessageBoxService _messageBoxService;

        private readonly ISshService _sshService;
        private CpuInfo _cpuInfo;
        private IEnumerable<DiskUsage> _disksUsage;
        private MemoryUsage _memoryUsage;
        private IEnumerable<Upgradable> _upgradables;
        private IEnumerable<Framework.Mobile.Ssh.Std.Models.Process> _processes;
        private int _upgradablesCount;
        private int _processesCount;
        private TimeSpan _uptime;

        public MainPageViewModel(ISshService sshService, IMessageBoxService messageBoxService)
        {
            Title = Resource.Home;
            _sshService = sshService;
            _messageBoxService = messageBoxService;
            CmdState = RefreshCmd;
            _addHostCmd = new Command(
                AddHost);
            SetCommandForStateView(_addHostCmd);

            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(ChangeHost);
            UpdatesCmd = new Command(Updates);
            ShowProcessesCmd = new Command(ShowProcesses);
        }


        private class toto: IAsyncResult
        {
            public object AsyncState { get; }
            public WaitHandle AsyncWaitHandle { get; }
            public bool CompletedSynchronously { get; }
            public bool IsCompleted { get; }
        }


        protected override Task OnInternalAppearingAsync()
        {
            MessagingCenter.Subscribe<DataProvider, TableBase>(
                this, MessengerKeys.ItemChanged, async (sender, arg) =>
                {
                    if (arg is Host)
                        await InitHostAsync();
                });
            return base.OnInternalAppearingAsync();
        }

        protected override Task OnInternalDisappearingAsync()
        {
            //MessagingCenter.Unsubscribe<DataProvider, TableBase>(this, MessengerKeys.ItemChanged);
            return base.OnInternalDisappearingAsync();
        }

        private void ShowProcesses(object obj)
        {
            NavigationService.NavigateAsync(nameof(ProcessesPage));
            //if (obj == null) return;

            //if (obj is Process p)
            //{
            //    _sshService.RunCommand($"kill {p.Pid}");
            //}
        }

        public ObservableRangeCollection<Host> Hosts { get; } = new ObservableRangeCollection<Host>();
        public ICommand ManageHostsCmd { get; }
        public  ICommand ShowProcessesCmd { get; }
        public ICommand UpdatesCmd { get; }

        public ICommand ChangeHostCmd { get; }

        public int UpgradablesCount
        {
            get => _upgradablesCount;
            set => SetProperty(ref _upgradablesCount, value);
        }
        public int ProcessesCount
        {
            get => _processesCount;
            set => SetProperty(ref _processesCount, value);
        }
        public TimeSpan Uptime
        {
            get => _uptime;
            set => SetProperty(ref _uptime, value);
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

        public MemoryUsage MemoryUsage
        {
            get => _memoryUsage;
            set => SetProperty(ref _memoryUsage, value);
        }
        public IEnumerable<Framework.Mobile.Ssh.Std.Models.Process> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        public IEnumerable<Upgradable> Upgradables
        {
            get => _upgradables;
            set => SetProperty(ref _upgradables, value);
        }

        private void Updates()
        {
            NavigationService.NavigateAsync(nameof(AptUpdatesPage));
        }

        private async void ChangeHost()
        {
            var action = await _messageBoxService.ShowActionSheet(Resource.SelectHost, Resource.Cancel, null,
                Hosts.Select(h => $"{h.Id} - {h.HostName}").ToArray());

            if (action != Resource.Cancel)
            {
                var split = action.Split('-');
                var id = long.Parse(split[0]);
                await TryToConnect(Hosts.First(h => h.Id == id));
            }
            //var page = new PopupListViewPage();

            //await PopupNavigation.Instance.PushAsync(page);

            //var hosts = await DataProvider.GetHostsAsync();
            //var vm = new PopupPageItemActionViewModel<Host>(hosts);
            //var page = new PopupPageItemAction(vm);

            //await PopupNavigation.Instance.PushAsync(page);
            //var selected = vm.SelectedItems;
            //if (selected != null)
            //{
            //    await Login(selected);
            //}
        }

        private void Clear()
        {
            Upgradables = null;
            CpuInfo = null;
          
            DisksUsage = null;
            UpgradablesCount = 0;
            ProcessesCount = 0;
            Processes = null;
            Upgradables = null;
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
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

       
        private async Task InitHostAsync()
        {
            var hosts = await DataProvider.GetHostsAsync();
            if (!hosts.Any())
            {
                ShowErrorHostState();
            }
            else
            {
                Hosts.ReplaceRange(hosts);
                var l = Preferences.Get(PreferencesKeys.SelectedHostIdKey, 0L);
                if (l > 0)
                {
                    var findHost = Hosts.FirstOrDefault(h => h.Id.Value == l);

                    if (findHost != null)
                        await TryToConnect(findHost);
                    else
                        await TryToConnect(Hosts.First());
                }
                else
                {
                    await TryToConnect(Hosts.First());
                }
            }
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            await InitHostAsync().ConfigureAwait(false);
        }
        //protected override async Task OnInternalAppearingAsync()
        //{
        //    await InitHostAsync().ConfigureAwait(false);
        //}

        private async Task TryToConnect(Host host)
        {
            SetLabelsStateItem(Resource.PleaseWait, Resource.TryToConnect);
            ViewModelStateItem.IsRunning = true;

            try
            {
                await Login(host);
            }
            catch (Exception ex)
            {
                SetLabelsStateItem(Resource.Oups, Resource.CanTConnect);
                Clear();
            }

            ViewModelStateItem.IsRunning = false;
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

        private void SetSelectedIdHost(Host host)
        {
            Preferences.Set(PreferencesKeys.SelectedHostIdKey, host.Id.GetValueOrDefault());
        }

        private async Task Login(Host host)
        {
            SetSelectedIdHost(host);

            Title = host.HostName;
            var con = new SshConnection(host.Url, host.Port, host.UserName, host.Password);
            await _sshService.InitAsync(con).ConfigureAwait(false);

            SetLabelsStateItem(host.HostName, host.Url);

            //var result1 = await _sshService.ExecuteTaskAsync<DiskUsageBeanWhapper>(diskUsageRequest);

            await Task.WhenAll(GetCpuInfo(), GetUptime(), GetDisksUsage(), CheckMemoryUsage(), GetProcesses(), GetUpgradables());
           
        }

        private async Task GetUpgradables()
        {
            Upgradables = await _sshService.GetUpgradables();
            UpgradablesCount = _upgradables.Count();

            if (UpgradablesCount <= 0) //maybe we will need an update
            {
                var updated = await _sshService.UpdateAptList();
                if (updated)
                {
                    Upgradables = await _sshService.GetUpgradables();
                    UpgradablesCount = _upgradables.Count();
                }
            }
        }
        private async Task GetProcesses()
        {
            Processes = await _sshService.GetProcesses();
            ProcessesCount = _processes.Count();

           
        }
        private async Task Reboot()
        {
            await _sshService.Rebout();
        }

        private async Task Halt()
        {
            await _sshService.Halt();
        }
        private async Task GetCpuInfo()
        {
            CpuInfo = await _sshService.GetCpuInfo();
        }

        private async Task GetUptime()
        {
            Uptime = await _sshService.GetUptime();
        }

        private async Task GetDisksUsage()
        {
            DisksUsage = await _sshService.GetDisksUsage();
        }

        private async Task CheckMemoryUsage()
        {
            MemoryUsage = await _sshService.CheckMemoryUsage();
        }
    }
}