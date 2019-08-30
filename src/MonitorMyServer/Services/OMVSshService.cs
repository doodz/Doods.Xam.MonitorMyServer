using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Ssh.Std.Queries;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Openmedivault.Ssh.Std.Requests;
using Renci.SshNet;
using Renci.SshNet.Security.Cryptography;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{
    public interface IOMVSshBackgroundService
    {
        Task<bool> CheckRunningAsync(string filename);
        Task<Output<T>> GetOutputAsync<T>(string filename);
        Task<Output<string>> GetOutputAsync(string filename);
      
    }


    public interface IOMVSshService : IOMVSettingsService, IOMVSshBackgroundService
    {
        Task<IEnumerable<Devices>> GetDevices();
        Task<IEnumerable<OmvFilesystems>> GetFilesystems();

        Task<IEnumerable<ServicesStatus>> GetServicesStatus();

        Task<IEnumerable<Upgraded>> GetUpgraded();
        Task<IEnumerable<SystemInformation>> GetSystemInformation();

        Task<bool> IsRunning(string filename);
        Task<T> RunCmd<T>(ISshRequest request);
        Task<string> UpdateAptList();
        Task<string> UpgradeAptList(IEnumerable<string> lst);
        Task<Output<T>> GetOutput<T>(string filename, long pos);
        ScpClient GetScpClient();
        Task<IEnumerable<PluginInfo>> GetPlugins();
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

    public interface IOMVSettingsService
    {
        Task<PowerManagementSetting> GetPowerManagementSetting();
        Task<NetworkSetting> GetNetworkSetting();
        Task<TimeSetting> GetDateAndTimeSetting();
        Task<IEnumerable<string>> GetTimeZoneList();
        Task<AptSetting> GetAptSettings();

        Task<bool> SetPowerManagementSetting(PowerManagementSetting powerManagementSetting);
        Task<bool> SetNetworkSetting(NetworkSetting networkSetting);
        Task<bool> SetDateAndTimeSetting(TimeSetting timeSetting);

        Task<bool> SetAptSettings(AptSetting aptSetting);
    }

    public class OMVSshService : SshService, IOMVSshService
    {
        public OMVSshService(ILogger logger, IMapper mapper) : base(logger, mapper)
        {
        }

        public async Task<IEnumerable<Devices>> GetDevices()
        {
            var result = await RunCmd<ResponseArray<Devices>>(new DevicesRequest()).ConfigureAwait(false);
            return result.Data;
        }

        public Task<IEnumerable<OmvFilesystems>> GetFilesystems()
        {
            return RunCmd<IEnumerable<OmvFilesystems>>(new FilesystemsRequest());
        }

        public async Task<IEnumerable<ServicesStatus>> GetServicesStatus()
        {
            var result = await RunCmd<ResponseArray<ServicesStatus>>(new ServicesStatusRequest()).ConfigureAwait(false);
            return result.Data;
        }

        public Task<IEnumerable<Upgraded>> GetUpgraded()
        {
            return RunCmd<IEnumerable<Upgraded>>(new EnumerateUpgradedRequest());
        }

        public new Task<string> UpdateAptList()
        {
            return RunCmd<string>(new UpdateAptListRequest());
        }

        public Task<string> UpgradeAptList(IEnumerable<string> lst)
        {
            return RunCmd<string>(new UpgradeAptList(lst));
        }

        public Task<IEnumerable<SystemInformation>> GetSystemInformation()
        {
            return RunCmd<IEnumerable<SystemInformation>>(new SystemInformationRequest());
        }

        public async Task<bool> IsRunning(string filename)
        {
            var result = await RunCmd<IsRunning>(new IsRunningRequest(filename)).ConfigureAwait(false);
            return result.Running;
        }


        public async  Task<IEnumerable<PluginInfo>> GetPlugins()
        {
            var result =  await RunCmd<IEnumerable<OmvPlugins>>(new EnumeratePluginsRequest());
            return _mapper.Map<IEnumerable<OmvPlugins>, IEnumerable<PluginInfo>>(result);

        }

        public Task<string> GenerateRdd()
        {
            return RunCmd<string>(new GenerateRddRequest());
        }

        public Task<string> GetDisksBackground()
        {
            return RunCmd<string>(new GetListDickBackgroundRequest());
        }

        public Task<string> GetListCandidatesFileSystemBackground()
        {
            return RunCmd<string>(new GetListCandidatesFileSystemBackgroundRequest());
        }

        public Task<string> CreateFileSystemBackground(BaseOmvFilesystems newFilesystems)
        {
            return RunCmd<string>(new CreateFileSystemBackgroundRequest(newFilesystems));
        }

        public Task<string> GetListFileSystemBackground()
        {
            return RunCmd<string>(new GetListFileSystemBackgroundRequest());
        }

        public async Task<bool> UmountFileSystem(OmvFilesystems filesystem)
        {
            var obj = await RunCmd<object>(new UmountFileSystemBackgroundRequest(filesystem));
            return obj != null;
        }

        public async Task<bool> DeleteFileSystem(OmvFilesystems filesystem)
        {
            var obj = await RunCmd<object>(new DeleteFileSystemBackgroundRequest(filesystem));
            return obj != null;
        }

        public async Task<bool> MountFileSystem(OmvFilesystems filesystem)
        {
            var obj = await RunCmd<object>(new MountFileSystemBackgroundRequest(filesystem));
            return obj != null;
        }

        public async Task<IEnumerable<string>> ListRdd()
        {
            //var result = await RunCmd<string>(new ListRddRequest());
            var result = await RunCommand("ls  /var/lib/openmediavault/rrd");
            return result.Split(new[] { "\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public Task<PowerManagementSetting> GetPowerManagementSetting()
        {
            return RunCmd<PowerManagementSetting>(new GetPowerManagementSettingRequest());
        }

        public Task<NetworkSetting> GetNetworkSetting()
        {
            return RunCmd<NetworkSetting>(new GetNetworkSettingRequest());
        }

        public Task<TimeSetting> GetDateAndTimeSetting()
        {
            return RunCmd<TimeSetting>(new GetDateAndTimeSettingRequest());
        }

        public Task<IEnumerable<string>> GetTimeZoneList()
        {
            return RunCmd<IEnumerable<string>>(new GetTimeZoneListRequest());
        }

        public Task<AptSetting> GetAptSettings()
        {
            return RunCmd<AptSetting>(new GetAptSettingRequest());
        }

        public async Task<bool> SetPowerManagementSetting(PowerManagementSetting powerManagementSetting)
        {
            var obj = await RunCmd<object>(new SetPowerManagementSettingRequest(powerManagementSetting));
            return obj != null;
        }

        public async Task<bool> SetNetworkSetting(NetworkSetting networkSetting)
        {
            var obj = await RunCmd<object>(new SetNetworkSettingRequest(networkSetting));
            return obj != null;
        }

        public async Task<bool> SetDateAndTimeSetting(TimeSetting timeSetting)
        {
            var obj = await RunCmd<object>(new SetDateAndTimeSettingRequest(timeSetting));
            return obj != null;
        }

        public async Task<bool> SetAptSettings(AptSetting aptSetting)
        {
            var obj = await RunCmd<object>(new SetAptSettingRequest(aptSetting));
            return obj != null;
        }


        public async Task<T> RunCmd<T>(ISshRequest request)
        {
            var isRunningPidResult = await ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            LogError(isRunningPidResult);
            return isRunningPidResult.Data;
        }


        private void LogError(ISshResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                if (response.ErrorException != null)
                {
                    Logger.Error(response.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + response.Content + Environment.NewLine +
                                 response.ErrorException.Message);
                    return;
                }


                var omvError = response.Request.Handler.DeserializeError<ResponseError<OMVError>>(response);
                if (omvError != null)
                    Logger.Error(response.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + omvError.Error.Message);
                else if (response.ErrorMessage != null)
                    Logger.Error(response.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + response.ErrorMessage);
            }
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
            var result = await GetOutput<string>(filename, 0);
            if (result.Running)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                return await GetOutputAsync<T>(filename);
            }

            result.Content = result.Content.Replace(@"\\\",string.Empty);
            result.Content = result.Content.Replace(@"\""", "\"");
            var toto = new Output<T>();

            toto.Filename = result.Filename;
            toto.Pos = result.Pos;
            toto.Running = result.Running;
            var serializer = new OmvSerializer();
            toto.Content = serializer.Deserialize<T>(result.Content);

            return toto;
        }

        public async Task<Output<string>> GetOutputAsync(string filename)
        {
            var result = await GetOutput<string>(filename, 0);
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

        public Task<string> ApplyChanges()
        {
            return RunCmd<string>(new ApplyChangesBgRequest());
        }

        public Task<string> InstallPlugins(IEnumerable<string> lst)
        {
            return RunCmd<string>(new InstallPlugin(lst));
        }

        public Task<string> RemovePlugins(IEnumerable<string> lst)
        {
            return RunCmd<string>(new RemovePlugin(lst));
        }

        public Task<Output<T>> GetOutput<T>(string filename, long pos)
        {
            return RunCmd<Output<T>>(new GetOutputRequest(filename, pos));
        }
    }
}