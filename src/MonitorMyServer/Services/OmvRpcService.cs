﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Enums;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.std.Data.V5;
using Doods.Openmedivault.Ssh.Std.Requests;
using Renci.SshNet;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{
    public class OmvRpcService : IOmvService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly OmvAptClient _omvAptClient;
        private readonly OmvConfigClient _omvConfigClient;

        private readonly OmvDiskMgmtClient _omvDiskMgmtClient;
        private readonly OmvExecClient _omvExecClient;

        private readonly OmvFileSystemMgmtClient _omvFileSystemMgmtClient;
        private readonly OmvNetworkClient _omvNetworkClient;
        private readonly OmvPluginClient _omvPluginClient;

        private readonly OmvPowerMgmtClient _omvPowerMgmtClient;
        private readonly OmvRrdClient _omvRrdClient;

        private readonly OmvSystemClient _omvSystemClient;

        private readonly OmvWebGuiClient _omvWebGuiClient;
        private readonly OmvServicesClient _omvServicesClient;

        public OmvRpcService(IRpcClient client, ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            var client1 = client;
            _omvSystemClient = new OmvSystemClient(client1);
            _omvPluginClient = new OmvPluginClient(client1);
            _omvAptClient = new OmvAptClient(client1);
            _omvConfigClient = new OmvConfigClient(client1);

            _omvDiskMgmtClient = new OmvDiskMgmtClient(client1);
            _omvExecClient = new OmvExecClient(client1);
            _omvServicesClient = new OmvServicesClient(client1);
            _omvFileSystemMgmtClient = new OmvFileSystemMgmtClient(client1);
            _omvNetworkClient = new OmvNetworkClient(client1);

            _omvPowerMgmtClient = new OmvPowerMgmtClient(client1);
            _omvRrdClient = new OmvRrdClient(client1);


            _omvWebGuiClient = new OmvWebGuiClient(client1);
        }


        public void SetOMVVersion(OMVVersion version)
        {
            _omvSystemClient.SetOMVVersion(version);
        }

        public OMVVersion GetRpcVersion()
        {
            return _omvSystemClient.GetRpcVersion();
        }

        public Task<IEnumerable<Devices>> GetDevices()
        {
            return _omvNetworkClient.GetDevices();
        }

        public Task<IEnumerable<OmvFilesystems>> GetFilesystems()
        {
            return _omvFileSystemMgmtClient.GetFilesystems();
        }

        public async Task<IEnumerable<ServicesStatus>> GetServicesStatus()
        {
            var result = await _omvServicesClient.GetServicesStatus();
            return result.Data;
        }

        public Task<IEnumerable<Upgraded>> GetUpgraded()
        {
            return _omvAptClient.GetUpgraded();
        }

        public Task<OMVInformations> GetSystemInformations()
        {
            return _omvSystemClient.GetSystemInformations();
        }

        public Task<bool> IsRunning(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<T> RunCmd<T>(ISshRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAptList()
        {
            return _omvAptClient.UpdateAptList();
        }

        public Task<string> UpgradeAptList(IEnumerable<string> lst)
        {
            return _omvAptClient.UpgradeAptList(lst);
        }

        public Task<Output<T>> GetOutput<T>(string filename, long pos)
        {
            return _omvExecClient.GetOutput<T>(filename, pos);
        }

        public ScpClient GetScpClient()
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateRdd()
        {
            return _omvRrdClient.GenerateRdd();
        }

        public Task<string> GetDisksBackground()
        {
            return _omvDiskMgmtClient.GetDisksBackground();
        }

        public Task<string> GetListCandidatesFileSystemBackground()
        {
            return _omvFileSystemMgmtClient.GetCandidatesBg();
        }

        public Task<string> CreateFileSystemBackground(BaseOmvFilesystems newFilesystems)
        {
            return _omvFileSystemMgmtClient.CreateFileSystemBackground(newFilesystems);
        }

        public Task<string> GetListFileSystemBackground()
        {
            return _omvFileSystemMgmtClient.GetListFileSystemBackground();
        }

        public Task<bool> UmountFileSystem(OmvFilesystems filesystem)
        {
            return _omvFileSystemMgmtClient.UmountFileSystem(filesystem);
        }

        public Task<bool> DeleteFileSystem(OmvFilesystems filesystem)
        {
            return _omvFileSystemMgmtClient.DeleteFileSystem(filesystem);
        }

        public Task<bool> MountFileSystem(OmvFilesystems filesystem)
        {
            return _omvFileSystemMgmtClient.MountFileSystem(filesystem);
        }

        public Task<IEnumerable<string>> ListRdd()
        {
            throw new NotImplementedException();
        }

        public Task<string> ApplyChanges()
        {
            return _omvConfigClient.ApplyChangesBg();
        }

        public Task<string> InstallPlugins(IEnumerable<string> lst)
        {
            return _omvPluginClient.InstallPlugins(lst);
        }

        public Task<string> RemovePlugins(IEnumerable<string> lst)
        {
            return _omvPluginClient.RemovePlugins(lst);
        }

        public async Task<IEnumerable<PluginInfo>> GetPlugins()
        {
            var result = await _omvPluginClient.GetPlugins();
            return _mapper.Map<IEnumerable<OmvPlugins>, IEnumerable<PluginInfo>>(result);
        }

        public Task<PowerManagementSetting> GetPowerManagementSetting()
        {
            return _omvPowerMgmtClient.GetSettings();
        }

        public Task<NetworkSetting> GetNetworkSetting()
        {
            return _omvNetworkClient.GetSettings();
        }

        public Task<TimeSetting> GetDateAndTimeSetting()
        {
            return _omvSystemClient.GetDateAndTimeSetting();
        }

        public Task<IEnumerable<string>> GetTimeZoneList()
        {
            return _omvSystemClient.GetTimeZoneList();
        }

        public Task<AptSetting> GetAptSettings()
        {
            return _omvAptClient.GetSettings();
        }

        public Task<WebGuiSetting> GetWebGuiSettings()
        {
            return _omvWebGuiClient.GetSettings();
        }

        public async Task<bool> SetPowerManagementSetting(PowerManagementSetting powerManagementSetting)
        {
            var result = await _omvPowerMgmtClient.SetSettings(powerManagementSetting);
            return result != null;
        }

        public async Task<bool> SetNetworkSetting(NetworkSetting networkSetting)
        {
            var result = await _omvNetworkClient.SetSettings(networkSetting);
            return result != null;
        }

        public async Task<bool> SetDateAndTimeSetting(TimeSetting timeSetting)
        {
            var result = await _omvSystemClient.SetDateAndTimeSetting(timeSetting);
            return result != null;
        }

        public async Task<bool> SetAptSettings(AptSetting aptSetting)
        {
            var result = await _omvAptClient.SetSettings(aptSetting);
            return result != null;
        }

        public async Task<bool> SetWebGuiSettings(WebGuiSetting webGuiSetting)
        {
            var result = await _omvWebGuiClient.SetSettings(webGuiSetting);
            return result != null;
        }

        public Task<bool> CheckRunningAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<Output<T>> GetOutputAsync<T>(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<Output<string>> GetOutputAsync(string filename)
        {
            throw new NotImplementedException();
        }
    }
}