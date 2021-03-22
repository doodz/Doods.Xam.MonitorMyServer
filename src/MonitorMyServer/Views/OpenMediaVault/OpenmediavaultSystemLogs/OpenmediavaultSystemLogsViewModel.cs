using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSystemLogs
{
    public class LogLineModel
    {
        private LogLine _logLine;


        public LogLineModel(LogLine logline)
        {
            _logLine = logline;
            Rownum = logline.Rownum;
            Ts = logline.Ts;
            //Date = logline.Date;
            Hostname = logline.Hostname;

            var tmp = logline.Message.Split(':');
            Title = tmp[0];
            Message = logline.Message.Replace(Title + ':', string.Empty);
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(logline.Ts);
            Date = dateTimeOffset.UtcDateTime;
        }

        public bool IsLast { get; set; } = true;
        public long Rownum { get; }

        public long Ts { get; }

        public DateTime Date { get; }

        public string Title { get; }

        public string Hour => Date.ToString("T");

        public string Hostname { get; }

        public string Message { get; }
    }

    public class OpenmediavaultSystemLogsViewModel : ViewModelWhithState
    {
        private readonly IOmvService _sshService;
        private OmvLogFileEnum _selectedLogFile;

        public OpenmediavaultSystemLogsViewModel(IOmvService sshService)
        {
            _sshService = sshService;
            DeleteItemCommand = new Command(DeleteItem);
            DownloadItemCommand = new Command(DownloadItem);
            SyncItemCommand = new Command(SyncItem);
        }

        public ObservableRangeCollection<LogLineModel> LogsLines { get; } = new ObservableRangeCollection<LogLineModel>();

        public ICommand DeleteItemCommand { get; }
        public ICommand DownloadItemCommand { get; }
        public ICommand SyncItemCommand { get; }

        public OmvLogFileEnum SelectedLogFile
        {
            get => _selectedLogFile;
            set => SetProperty(ref _selectedLogFile, value, async () => await GetLogs(), null);
        }

        public string LasteDate { get; set; }

        private async void SyncItem(object o)
        {
            await GetLogs();
        }

        private void DownloadItem()
        {
        }

        private void DeleteItem()
        {
        }

        protected override async Task OnInternalAppearingAsync()
        {
            await RefreshData();
            await base.OnInternalAppearingAsync();
        }

        private Task RefreshData()
        {
            return Task.WhenAll(GetLogs());
        }

        private async Task GetLogs()
        {
            SetLabelsStateItem(Resource.Dowloading, Resource.PleaseWait);
            ViewModelStateItem.IsRunning = true;
            try
            {
                var items = await _sshService.GetLogFile(_selectedLogFile);

                var toto = items.Select(i => new LogLineModel(i));


                if (items.Any())
                {
                    LasteDate = items.First().Date;
                    toto.Last().IsLast = false;
                }
                else
                {
                    LasteDate = string.Empty;
                }

                LogsLines.ReplaceRange(toto);
            }
            catch
            {
                LogsLines.Clear();
                LasteDate = string.Empty;
            }

            ViewModelStateItem.IsRunning = false;
        }
    }
}