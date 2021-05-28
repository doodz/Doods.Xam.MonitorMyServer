using System;
using System.Threading.Tasks;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class TimeSettingViewModel : BaseSettingsViewModel<TimeSetting>
    {
        private bool _ntpenable;
        private string _ntptimeservers;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;
        private string _selectedTimeZone;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public string SelectedTimeZone
        {
            get => _selectedTimeZone;
            set => SetProperty(ref _selectedTimeZone, value);
        }

        public string Ntptimeservers
        {
            get => _ntptimeservers;
            set => SetProperty(ref _ntptimeservers, value);
        }

        public bool Ntpenable
        {
            get => _ntpenable;
            set => SetProperty(ref _ntpenable, value);
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetProperty(ref _selectedTime, value);
        }

        public ObservableRangeCollection<string> TimeZoneList { get; } = new ObservableRangeCollection<string>();

        public override async Task<bool> SaveSettings()
        {
            var obj = new TimeSetting();
            obj.Ntpenable = Ntpenable;
            obj.Ntptimeservers = Ntptimeservers;

            obj.Timezone = SelectedTimeZone;
            //obj.SelectedTime = obj.Date.Iso8601.TimeOfDay;
            //obj.SelectedDate = obj.Date.Iso8601.Date;
            var result = await SshService.SetDateAndTimeSetting(obj);
            return result;
        }

        private async Task GetTimeZoneList()
        {
            var obj = await SshService.GetTimeZoneList();
            TimeZoneList.ReplaceRange(obj);
        }

        public override async Task<TimeSetting> GetSettings()
        {
            await GetTimeZoneList();

            var obj = await SshService.GetDateAndTimeSetting();
            Ntpenable = obj.Ntpenable;
            Ntptimeservers = obj.Ntptimeservers;
            SelectedDate = obj.Date.Iso8601.Date;
            SelectedTimeZone = obj.Timezone;
            SelectedTime = obj.Date.Iso8601.TimeOfDay;
            Settings = obj;
            return obj;
        }
    }
}