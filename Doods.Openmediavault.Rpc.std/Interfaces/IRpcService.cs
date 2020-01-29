using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Data.V5;
using Doods.Openmediavault.Rpc.std.Interfaces;
using Renci.SshNet;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public interface IRpcService : IOMVSettingsService, IOMVSshBackgroundService
    {
        Task<IEnumerable<Devices>> GetDevices();
        Task<IEnumerable<OmvFilesystems>> GetFilesystems();

        Task<IEnumerable<ServicesStatus>> GetServicesStatus();

        Task<IEnumerable<Upgraded>> GetUpgraded();

        Task<OMVInformations> GetSystemInformations();
        Task<bool> IsRunning(string filename);
        Task<T> RunCmd<T>(ISshRequest request);
        Task<string> UpdateAptList();
        Task<string> UpgradeAptList(IEnumerable<string> lst);
        Task<Output<T>> GetOutput<T>(string filename, long pos);
        ScpClient GetScpClient();
        //Task<IEnumerable<PluginInfo>> GetPlugins();
        Task<string> GenerateRdd();
        Task<string> GetDisksBackground();
        Task<string> GetListCandidatesFileSystemBackground();
        Task<string> CreateFileSystemBackground(BaseOmvFilesystems newFilesystems);
        Task<string> GetListFileSystemBackground();
        Task<bool> UmountFileSystem(OmvFilesystems filesystem);
        Task<bool> DeleteFileSystem(OmvFilesystems filesystem);
        Task<bool> MountFileSystem(OmvFilesystems filesystem);
        Task<IEnumerable<string>> ListRdd();

        Task<string> ApplyChanges();

        //Task<string> RunCommand(string cmd);
        Task<string> InstallPlugins(IEnumerable<string> lst);
        Task<string> RemovePlugins(IEnumerable<string> lst);
    }
}