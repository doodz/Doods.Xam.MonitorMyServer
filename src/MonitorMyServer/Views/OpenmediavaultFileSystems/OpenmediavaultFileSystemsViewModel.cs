using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems
{
    internal class OpenmediavaultFileSystemsViewModel : ViewModelWhithState
    {
       
        private readonly IOmvService _sshService;

        public OpenmediavaultFileSystemsViewModel(IOmvService sshService)
        {
            _sshService = sshService;
           
            AddItemCommand = new Command(AddItem);
            MountFileSystemCmd = new Command(MountFileSystem, o =>
            {
                if (o is OmvFilesystems filesystem) return filesystem.CanMount;

                return false;
            });
            UmountFileSystemCmd = new Command(UmountFileSystem, o =>
            {
                if (o is OmvFilesystems filesystem) return filesystem.CanUmount;

                return false;
            });
            DeleteFileSystemCmd = new Command(DeleteFileSystem, o =>
            {
                if (o is OmvFilesystems filesystem) return filesystem.CanDelete;

                return false;
            });
        }

        public ICommand MountFileSystemCmd { get; }
        public ICommand UmountFileSystemCmd { get; }
        public ICommand DeleteFileSystemCmd { get; }

        public ObservableRangeCollection<OmvFilesystems> Filesystems { get; } =
            new ObservableRangeCollection<OmvFilesystems>();

        public ICommand AddItemCommand { get; }

        private async void UmountFileSystem(object o)
        {
            if (o is OmvFilesystems filesystem)
                await ViewModelStateItem.RunActionAsync(async () =>
                    {
                        await _sshService.UmountFileSystem(filesystem);
                        var fileneame = await _sshService.ApplyChanges();
                        await _sshService.CheckRunningAsync(fileneame);
                        await RefreshData();
                    },
                    () => SetLabelsStateItem("FileSystemMgmt", "Apply changes"),
                    () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
        }

        private async void DeleteFileSystem(object o)
        {
            if (o is OmvFilesystems filesystem)
                await ViewModelStateItem.RunActionAsync(async () =>
                    {
                        await _sshService.DeleteFileSystem(filesystem);
                        var fileneame = await _sshService.ApplyChanges();
                        await _sshService.CheckRunningAsync(fileneame);
                        await RefreshData();
                    },
                    () => SetLabelsStateItem("FileSystemMgmt", "Apply changes"),
                    () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
        }

        private async void MountFileSystem(object o)
        {
            if (o is OmvFilesystems filesystem)
                await ViewModelStateItem.RunActionAsync(async () =>
                    {
                        await _sshService.MountFileSystem(filesystem);
                        var fileneame = await _sshService.ApplyChanges();
                        await _sshService.CheckRunningAsync(fileneame);
                        await RefreshData();
                    },
                    () => SetLabelsStateItem("FileSystemMgmt", "Apply changes"),
                    () => { SetLabelsStateItem("FileSystemMgmt", "done!"); });
        }

        protected override async Task OnInternalAppearingAsync()
        {
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem("FileSystemMgmt", "Get file systems"),
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
            var result = await _sshService.GetOutputAsync<ResponseArray<OmvFilesystems>>(filename);
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
                Text = Openmediavault.Mobile.Std.Resources.openmediavault.Create,
                IsPrimary = true,
                Command = AddItemCommand,
                Icon = image3
            };
        }
    }
}