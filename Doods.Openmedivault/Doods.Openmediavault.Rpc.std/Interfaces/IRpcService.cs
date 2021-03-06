﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.Std.Data.V5;
using Doods.Openmediavault.Rpc.Std.Enums;
using Renci.SshNet;

namespace Doods.Openmediavault.Rpc.Std.Interfaces
{
    public interface IRrdService
    {
        Task<string> GenerateRdd();
        Task<IEnumerable<byte[]>> GetRrdFiles(IEnumerable<string> filesPaths);
        Task<byte[]> GetRrdFile(string filePath);
        Task<IEnumerable<string>> ListRdd();
    }

    public interface IRpcService : IOMVSettingsService, IOMVSshBackgroundService, IRrdService, IOmvNetworkClient
    {
        Task<IEnumerable<OmvFilesystems>> GetFilesystems();

        Task<IEnumerable<ServicesStatus>> GetServicesStatus();

        Task<IEnumerable<Upgraded>> GetUpgraded();

        Task<OMVInformations> GetSystemInformations();
        Task<bool> IsRunning(string filename);
        Task<T> RunCmd<T>(ISshRequest request);
        Task<string> UpdateAptList();
        Task<string> UpgradeAptList(IEnumerable<string> lst);
        Task<Output<T>> GetOutput<T>(string filename, int pos);

        ScpClient GetScpClient();
        //Task<IEnumerable<PluginInfo>> GetPlugins();

        Task<string> GetDisksBackground();
        Task<string> GetListCandidatesFileSystemBackground();
        Task<string> CreateFileSystemBackground(BaseOmvFilesystems newFilesystems);
        Task<string> GetListFileSystemBackground();
        Task<bool> UmountFileSystem(OmvFilesystems filesystem);
        Task<bool> DeleteFileSystem(OmvFilesystems filesystem);
        Task<bool> MountFileSystem(OmvFilesystems filesystem);


        Task<string> ApplyChanges();

        //Task<string> RunCommand(string cmd);
        Task<string> InstallPlugins(IEnumerable<string> lst);
        Task<string> RemovePlugins(IEnumerable<string> lst);
        Task<IEnumerable<LogLine>> GetLogFile(OmvLogFileEnum logfile);
    }
}