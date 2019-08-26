using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems
{
    internal class OpenmediavaultFileSystemsViewModel : ViewModelWhithState
    {
        private readonly IOMVSshBackgroundService _backgroundService;

        private readonly IOMVSshService _sshService;

        public OpenmediavaultFileSystemsViewModel(IOMVSshService sshService, IOMVSshBackgroundService backgroundService)
        {
            _sshService = sshService;
            _backgroundService = backgroundService;
            AddItemCommand = new Command(AddItem);
        }

        public ObservableRangeCollection<OmvFilesystems> Filesystems { get; } =
            new ObservableRangeCollection<OmvFilesystems>();

        public ICommand AddItemCommand { get; }

        protected override async Task OnInternalAppearingAsync()
        {
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); }, 
                () => SetLabelsStateItem("FileSystemMgmt", $"Get file systems"),
                () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
            await base.OnInternalAppearingAsync();
        }

        private void AddItem(object obj)
        {
            NavigationService.NavigateAsync(nameof(OpenmediavaultAddFileSystemsPage));
        }

        protected async Task RefreshData()
        {
            await Task.WhenAll(GetListFileSystem());
        }

        private async Task GetListFileSystem()
        {
            var filename = await _sshService.GetListFileSystemBackground();
            var result = await _backgroundService.GetOutputAsync<ResponseArray<OmvFilesystems>>(filename);
            Filesystems.ReplaceRange(result.Content.Data);
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {
            var image1 = SvgIconTarget.AddBox.ResourceFile;
            var image2 = new SvgCachedImage
            {
                Source = SvgIconTarget.AddBox.ResourceFile,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100,
                DownsampleToViewSize = true,
                Aspect = Aspect.AspectFill,
                TransformPlaceholders = false,
                LoadingPlaceholder = "loading.png",
                ErrorPlaceholder = "error.png"
            };


            var image3 = new FileImageSource();
            image3.File = image1;

            yield return new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Add",
                IsPrimary = true,
                Command = AddItemCommand,
                Icon = image3
            };
        }
    }
}