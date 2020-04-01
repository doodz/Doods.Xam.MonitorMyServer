using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem
{
    public class OpenmediavaultAddFileSystemViewModel : ViewModelWhithState
    {
        private readonly IOMVSshBackgroundService _backgroundService;
        private readonly IOmvService _sshService;

        private string _label;

        private string _result;
        private CandidateFileSystem _selectedCandidate;
        private string _selectedFileSystem;

        public OpenmediavaultAddFileSystemViewModel(IOmvService sshService,
            IOMVSshBackgroundService backgroundService)
        {
            _sshService = sshService;
            _backgroundService = backgroundService;
            SaveCmd = new Command(async () => await Save());
        }

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public ICommand SaveCmd { get; }

        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ObservableRangeCollection<CandidateFileSystem> LstCandidateFileSystem { get; } =
            new ObservableRangeCollection<CandidateFileSystem>();

        public ObservableRangeCollection<string> LstFileSystem { get; } =
            new ObservableRangeCollection<string>(new[] {"BTRFS", "EXT3", "EXT4", "XFS", "JFS"});

        public string SelectedFileSystem
        {
            get => _selectedFileSystem;
            set => SetProperty(ref _selectedFileSystem, value);
        }

        public CandidateFileSystem SelectedCandidate
        {
            get => _selectedCandidate;
            set => SetProperty(ref _selectedCandidate, value);
        }

        protected override async Task OnInternalAppearingAsync()
        {
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem("FileSystemMgmt", "Get candidates"),
                () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetListFileSystem());
        }

        private async Task GetListFileSystem()
        {
            var filename = await _sshService.GetListCandidatesFileSystemBackground();
            var result = await _backgroundService.GetOutputAsync<CandidateFileSystem[]>(filename);
            LstCandidateFileSystem.ReplaceRange(result.Content);
        }


        private async Task Save()
        {
            var newFileSystem = new BaseOmvFilesystems();
            newFileSystem.Label = Label;
            newFileSystem.Type = SelectedFileSystem;
            newFileSystem.Devicefile = SelectedCandidate.Devicefile /*.Replace("/",@"\\\/");*/;

            await ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _sshService.CreateFileSystemBackground(newFileSystem);
                    var result = await _backgroundService.GetOutputAsync(filename);
                    Result = result.Content;
                },
                () => SetLabelsStateItem("FileSystemMgmt", "Create file system"),
                () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
        }
    }
}