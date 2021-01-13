using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Processes2;
using Doods.Xam.MonitorMyServer.Views.Tests;
using Xamarin.Forms;
using Resource = Doods.Xam.MonitorMyServer.Resx.Resource;

namespace Doods.Xam.MonitorMyServer.Views
{
    public class MainPageViewModel : ViewModelWhithState
    {
        private readonly ICommand _addHostCmd;
        private readonly ISshService _sshService;
        private IEnumerable<string> _groups;
        private CpuInfo _cpuInfo;
        private Hostnamectl _hostnamectl;
       
        private MemoryUsage _memoryUsage;
        private IEnumerable<Process> _processes;
        private int _processesCount;
        private IEnumerable<Upgradable> _upgradables;
        private int _upgradablesCount;
        private TimeSpan _uptime;
        private bool _canSudo;
        public MainPageViewModel(ISshService sshService, IConfiguration configuration)
        {
            Title = Resource.Home;
            _sshService = sshService;

            CmdState = RefreshCmd;
            _addHostCmd = new Command(
                AddHost);
            SetCommandForStateView(_addHostCmd);

            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(ChangeHost);
            UpdatesCmd = new Command(Updates);
            ShowProcessesCmd = new Command(ShowProcesses);
            ShowTestsPageCmd = new Command(() => NavigationService.NavigateAsync(nameof(TestPage)));
            BannerId = configuration.AdsKey;
        }

        public ICommand ShowTestsPageCmd { get; }
        public string BannerId { get; }

        //public ObservableRangeCollection<Host> Hosts { get; } = new ObservableRangeCollection<Host>();
        public ICommand ManageHostsCmd { get; }
        public ICommand ShowProcessesCmd { get; }
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

        public bool CanSudo
        {
            get => _canSudo;
            set => SetProperty(ref _canSudo, value);
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

        public Hostnamectl Hostnamectl
        {
            get => _hostnamectl;
            set => SetProperty(ref _hostnamectl, value);
        }
        public IEnumerable<string> Groups 
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }
       

        public MemoryUsage MemoryUsage
        {
            get => _memoryUsage;
            set => SetProperty(ref _memoryUsage, value);
        }

        public IEnumerable<Process> Processes
        {
            get => _processes;
            set => SetProperty(ref _processes, value);
        }

        public IEnumerable<Upgradable> Upgradables
        {
            get => _upgradables;
            set => SetProperty(ref _upgradables, value);
        }


        //protected override Task OnInternalAppearingAsync()
        //{
        //    MessagingCenter.Subscribe<ConnctionService, Host>(
        //        this, MessengerKeys.ItemChanged, async (sender, arg) =>
        //        {

        //                await InitHostAsync();
        //        });
        //    return base.OnInternalAppearingAsync();
        //}

        protected override Task OnInternalDisappearingAsync()
        {
            //MessagingCenter.Unsubscribe<DataProvider, TableBase>(this, MessengerKeys.ItemChanged);
            return base.OnInternalDisappearingAsync();
        }

        private void ShowProcesses(object obj)
        {
            NavigationService.NavigateAsync(nameof(ProcessesPage));
        }

        private void Updates()
        {
            NavigationService.NavigateAsync(nameof(AptUpdatesPage));
        }

        private async void ChangeHost()
        {
            var connctionService = App.Container.Resolve<ConnctionService>();
            await connctionService.ChangeHostTask();
        }

        private void Clear()
        {
            Upgradables = null;
            CpuInfo = null;
           
            UpgradablesCount = 0;
            ProcessesCount = 0;
            Processes = null;
            Upgradables = null;
            Hostnamectl = null;
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
        }

        private void AddHost()
        {
            NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage));
        }

        //protected override void OnInitializeLoading(LoadingContext context)
        //{
        //    //viewModelStateItem.ShowCurrentCmd = _addHostCmd;
        //    //viewModelStateItem.Color = Color.Blue;
        //}


        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    async () =>
                    {
                        SetLabelsStateItem(openmediavault.Server, openmediavault.SystemInformation);
                        
                    },
                    () =>
                    {
                        UpdateHistory();
                        SetLabelsStateItem(openmediavault.Server, openmediavault.Done___);
                    });
            }
            catch (Exception e)
            {
                SetLabelsStateItem(openmediavault.Error, e.Message);
            }

            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {

            if (!_sshService.IsConnected())
            {
                SetLabelsStateItem(openmediavault.Info, openmediavault.NoUsersConnected);
                return Task.FromResult(0);
            }

            return Task.WhenAll(GetGroups(),GetCpuInfo(), GetHostnamectl(), GetUptime(),  CheckMemoryUsage(), GetProcesses(),
                GetUpgradables());
        }

        //private async Task InitHostAsync()
        //{
        //    SetLabelsStateItem(Resource.PleaseWait, Resource.TryToConnect);
        //    ViewModelStateItem.IsRunning = true;
        //    var connctionService = App.Container.Resolve<ConnctionService>();

        //    //Can't load host onstartup app :/.
        //    if (!connctionService.Hosts.Any())
        //        await connctionService.Init();

        //    if (!connctionService.Hosts.Any())
        //        ShowErrorHostState();
        //    else
        //        try
        //        {
        //            await Login(connctionService.CurrentHost);
        //        }
        //        catch
        //        {
        //            SetLabelsStateItem(Resource.Oups, Resource.CanTConnect);
        //            Clear();
        //        }

        //    ViewModelStateItem.IsRunning = false;
        //}

        //protected override async Task InternalLoadAsync(LoadingContext context)
        //{
        //    if (!_sshService.IsConnected())
        //        await InitHostAsync();
        //}


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
        }

        //private async Task Login(Host host)
        //{
        //    Title = host.HostName;
        //    SetLabelsStateItem(host.HostName, host.Url);
        //    await Task.WhenAll(GetCpuInfo(), GetUptime(), GetDisksUsage(), CheckMemoryUsage(), GetProcesses(),
        //        GetUpgradables());
        //}

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


        private async Task GetGroups()
        {

            Groups = await _sshService.GetGroups();
            CanSudo = Groups.Any(g => g.ToLower() == "sudo");
        }

        private async Task GetCpuInfo()
        {
            CpuInfo = await _sshService.GetCpuInfo();
        }


        private async Task GetHostnamectl()
        {
            Hostnamectl = await _sshService.GetHostnamectl();
        }
        
        private async Task GetUptime()
        {
            Uptime = await _sshService.GetUptime();
        }

      

        private async Task CheckMemoryUsage()
        {
            MemoryUsage = await _sshService.CheckMemoryUsage();
        }

        private void UpdateHistory()
        {
            var historyService = App.Container.Resolve<IHistoryService>();
            if (historyService.CurrentHistoryItem == null)
                return;
            historyService.CurrentHistoryItem.NombrerPackargeCanBeUpdted = UpgradablesCount;
            historyService.CurrentHistoryItem.LastUpdate = DateTime.Now;
            historyService.SetHistoryAsync(historyService.CurrentHistoryItem.HostId, historyService.CurrentHistoryItem);
        }

        //private class toto : IAsyncResult
        //{
        //    public object AsyncState { get; }
        //    public WaitHandle AsyncWaitHandle { get; }
        //    public bool CompletedSynchronously { get; }
        //    public bool IsCompleted { get; }
        //}
    }
}