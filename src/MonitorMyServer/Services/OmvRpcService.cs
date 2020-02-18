using System;
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
        private readonly OmvLogFileClient _omvLogFileClient;
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
        private readonly IRpcClient _client;
        public OmvRpcService(IRpcClient client, ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _client = client;
            _omvSystemClient = new OmvSystemClient(_client);
            _omvPluginClient = new OmvPluginClient(_client);
            _omvAptClient = new OmvAptClient(_client);
            _omvConfigClient = new OmvConfigClient(_client);
            _omvDiskMgmtClient = new OmvDiskMgmtClient(_client);
            _omvExecClient = new OmvExecClient(_client);
            _omvServicesClient = new OmvServicesClient(_client);
            _omvFileSystemMgmtClient = new OmvFileSystemMgmtClient(_client);
            _omvNetworkClient = new OmvNetworkClient(_client);
            _omvPowerMgmtClient = new OmvPowerMgmtClient(_client);
            _omvRrdClient = new OmvRrdClient(_client);
            _omvWebGuiClient = new OmvWebGuiClient(_client);
            _omvLogFileClient = new OmvLogFileClient(client);
        }

        public async  Task<IEnumerable<LogLine>> Getlogs(string logfile)
        {
            var result =await _omvLogFileClient.GetList(logfile);
            return result.Data;
        }

        public void SetOMVVersion(OMVVersion version)
        {
            _omvSystemClient.SetOMVVersion(version);
        }

        public OMVVersion GetRpcVersion()
        {
            return _omvSystemClient.GetRpcVersion();
        }

        public async Task<IEnumerable<Devices>> GetDevices()
        {
            var result = await _omvNetworkClient.GetDevices();
            return result.Data;
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

        public async Task<bool> IsRunning(string filename)
        {
            var result =await  _omvExecClient.IsRunning(filename);
            return result.Running;
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

        public Task<Output<T>> GetOutput<T>(string filename, int pos)
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

        public async Task<bool> Connect(string username,string password)
        {
           return  await _client.LoginAsync(username, password);
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

        public async Task<bool> CheckRunningAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;
            if (!await IsRunning(filename)) return false;

            await Task.Delay(TimeSpan.FromSeconds(3));
            return await CheckRunningAsync(filename);
        }

        public async Task<Output<T>> GetOutputAsync<T>(string filename)
        {

            var result =await _omvExecClient.GetOutput<string>(filename, 0);
            if (result.Running)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                return await GetOutputAsync<T>(filename);
            }

            result.Content = result.Content.Replace(@"\\\", string.Empty);
            result.Content = result.Content.Replace(@"\""", "\"");
            var toto = new Output<T>();

            toto.Filename = result.Filename;
            toto.Pos = result.Pos;
            toto.Running = result.Running;
            var serializer = new OmvSerializer();
            toto.Content = serializer.Deserialize<T>(result.Content);

            return toto;
        }

        public async  Task<Output<string>> GetOutputAsync(string filename)
        {
            var result = await _omvExecClient.GetOutput<string>(filename, 0);
            if (result.Running)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                return await GetOutputAsync(filename);
            }

            result.Content = result.Content.Replace(@"\\\", string.Empty);
            result.Content = result.Content.Replace(@"\""", "\"");
            var toto = new Output<string>();

            toto.Filename = result.Filename;
            toto.Pos = result.Pos;
            toto.Running = result.Running;
            var serializer = new OmvSerializer();
            toto.Content = result.Content;

            return toto;
        }
    }
}