using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class OpenmediavaultSettingsViewModel : ViewModelWhithState
    {
        private readonly IOMVSshService _sshService;
        private bool _cpufreq;
        private string _domainname;
        private string _hostname;
        private bool _ntpenable;
        private string _ntptimeservers;
        private bool _partner;
        private bool _proposed;
        private DateTime _selectedDate;
        private PowerbtnAction _selectedPowerbtnAction;
        private TimeSpan _selectedTime;
        private string _selectedTimeZone;
        public ObservableRangeCollection<string> TimeZoneList { get; }= new ObservableRangeCollection<string>();

        public OpenmediavaultSettingsViewModel(IOMVSshService sshService)
        {
            _sshService = sshService;
            ResetPowerManagementSettingCmd = new Command(async () => await GetPowerManagementSetting());
            ResetNetworkSettingCmd = new Command(async () => await GetNetworkSetting());
            ResetDateAndTimeSettingCmd = new Command(async () => await GetDateAndTimeSetting());
            ResetAptSettingsCmd = new Command(async () => await GetAptSettings());

            SavePowerManagementSettingCmd = new Command(async () => await SavePowerManagementSetting());
            SaveNetworkSettingCmd = new Command(async () => await SaveNetworkSetting());
            SaveDateAndTimeSettingCmd = new Command(async () => await SaveDateAndTimeSetting());
            SaveAptSettingsCmd = new Command(async () => await SaveAptSettings());
        }

        public bool Partner
        {
            get => _partner;
            set => SetProperty(ref _partner, value);
        }

        public bool Proposed
        {
            get => _proposed;
            set => SetProperty(ref _proposed, value);
        }

        public bool Ntpenable
        {
            get => _ntpenable;
            set => SetProperty(ref _ntpenable, value);
        }

        public bool Cpufreq
        {
            get => _cpufreq;
            set => SetProperty(ref _cpufreq, value);
        }

        public PowerbtnAction SelectedPowerbtnAction
        {
            get => _selectedPowerbtnAction;
            set => SetProperty(ref _selectedPowerbtnAction, value);
        }

        public ITranslateService TranslateService { get; } =
            new TranslateExtension();

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetProperty(ref _selectedTime, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public string Ntptimeservers
        {
            get => _ntptimeservers;
            set => SetProperty(ref _ntptimeservers, value);
        }

        public string Domainname
        {
            get => _domainname;
            set => SetProperty(ref _domainname, value);
        }

        public string SelectedTimeZone
        {
            get => _selectedTimeZone;
            set => SetProperty(ref _selectedTimeZone, value);
        }

        public string Hostname
        {
            get => _hostname;
            set => SetProperty(ref _hostname, value);
        }


        public ICommand ResetPowerManagementSettingCmd { get; }
        public ICommand ResetNetworkSettingCmd { get; }
        public ICommand ResetDateAndTimeSettingCmd { get; }
        public ICommand ResetAptSettingsCmd { get; }
        public ICommand SavePowerManagementSettingCmd { get; }
        public ICommand SaveNetworkSettingCmd { get; }
        public ICommand SaveDateAndTimeSettingCmd { get; }
        public ICommand SaveAptSettingsCmd { get; }

        protected override async Task OnInternalAppearingAsync()
        {
            ViewModelStateItem.RunAction(async () => { await RefreshData(); });
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetTimeZoneList(), GetPowerManagementSetting(), GetNetworkSetting(),
                GetDateAndTimeSetting(), GetAptSettings());
        }

        private async Task GetAptSettings()
        {
            var obj = await _sshService.GetAptSettings();
            Partner = obj.Partner;
            Proposed = obj.Proposed;
        }

        private async Task GetTimeZoneList()
        {
            var obj = await _sshService.GetTimeZoneList();
            TimeZoneList.ReplaceRange(obj);
        }

        private async Task GetPowerManagementSetting()
        {
            var obj = await _sshService.GetPowerManagementSetting();

            Cpufreq = obj.Cpufreq;
            SelectedPowerbtnAction = obj.Powerbtn;
        }

        private async Task GetNetworkSetting()
        {
            var obj = await _sshService.GetNetworkSetting();
            Domainname = obj.Domainname;
            Hostname = obj.Hostname;
        }

        private async Task GetDateAndTimeSetting()
        {
            var obj = await _sshService.GetDateAndTimeSetting();
            Ntpenable = obj.Ntpenable;
            Ntptimeservers = obj.Ntptimeservers;
            SelectedDate = obj.Date.Iso8601.Date;
            SelectedTimeZone = obj.Timezone;
            SelectedTime = obj.Date.Iso8601.TimeOfDay;
        }


        private async Task SaveAptSettings()
        {
            var obj = new AptSetting();
            obj.Partner = Partner;
            obj.Proposed = Proposed;
            var result = await _sshService.SetAptSettings(obj);
        }


        private async Task SavePowerManagementSetting()
        {
            var obj = new PowerManagementSetting();
            obj.Cpufreq = Cpufreq;
            obj.Powerbtn = SelectedPowerbtnAction;
            var result = await _sshService.SetPowerManagementSetting(obj);
        }

        private async Task SaveNetworkSetting()
        {
            var obj = new NetworkSetting();
            obj.Domainname = Domainname;
            obj.Hostname = Hostname;
            var result = await _sshService.SetNetworkSetting(obj);
        }

        private async Task SaveDateAndTimeSetting()
        {
            var obj = new TimeSetting();
            obj.Ntpenable = Ntpenable;
            obj.Ntptimeservers = Ntptimeservers;

            obj.Timezone = SelectedTimeZone;
            //obj.SelectedTime = obj.Date.Iso8601.TimeOfDay;
            //obj.SelectedDate = obj.Date.Iso8601.Date;
            var result = await _sshService.SetDateAndTimeSetting(obj);
        }
    }
}