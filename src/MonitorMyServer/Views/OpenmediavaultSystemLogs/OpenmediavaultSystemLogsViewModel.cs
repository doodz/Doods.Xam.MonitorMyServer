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
    public class OpenmediavaultSystemLogsViewModel : ViewModelWhithState
    {
        private OmvLogFileEnum _selectedLogFile;
        public ObservableRangeCollection<LogLine> LogsLines { get; } = new ObservableRangeCollection<LogLine>();
        private readonly IOmvService _sshService;

        public OpenmediavaultSystemLogsViewModel(IOmvService sshService)
        {
            _sshService = sshService;
            DeleteItemCommand = new Command(DeleteItem);
            DownloadItemCommand = new Command(DownloadItem);
            SyncItemCommand = new Command(SyncItem);
        }

        public ICommand DeleteItemCommand { get; }
        public ICommand DownloadItemCommand { get; }
        public ICommand SyncItemCommand { get; }

        public OmvLogFileEnum SelectedLogFile
        {
            get => _selectedLogFile;
            set => SetProperty(ref _selectedLogFile, value, async ()=>await GetLogs(),null);
        }

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
                LogsLines.ReplaceRange(items);
            }
            catch
            {
                LogsLines.Clear();
            }

            ViewModelStateItem.IsRunning = false;
        }
    }
}