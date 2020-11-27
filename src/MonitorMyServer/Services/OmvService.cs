using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Enums;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.std.Data.V5;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Data.Nas;

namespace Doods.Xam.MonitorMyServer.Services
{
    [Obsolete("use OmvRpcService")]
    public class OmvService : SshService, IOmvService
    {
        private OMVVersion _OMVVersions;

        public OmvService(ILogger logger, IMapper mapper) : base(logger, mapper)
        {
        }

        public async Task<IEnumerable<Devices>> GetDevices()
        {
            var result = await RunCmd<ResponseArray<Devices>>(new DevicesRequest()).ConfigureAwait(false);
            return result.Data;
        }

        public Task<NetworkSetting> GetSettings()
        {
            throw new NotImplementedException();
        }

        public Task<object> SetSettings(NetworkSetting settings)
        {
            throw new NotImplementedException();
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

        public async Task<OMVInformations> GetSystemInformations()
        {
            await CheckRpcVersion();
            if (GetRPCVersion().Name == OMVVersions.Arrakis)
            {
                var lst = await GetSystemInformation();
                var obj = new OMVInformations {LegacyMode = true};

                foreach (var information in lst)
                    switch (information.Name)
                    {
                        case "ts":
                            obj.Ts = long.Parse(information.Value.SimpleStringValue);
                            break;
                        case "System time":
                            obj.Time = information.Value.SimpleStringValue;
                            break;
                        case "Hostname":
                            obj.Hostname = information.Value.SimpleStringValue;
                            break;
                        case "Version":
                            obj.Version = information.Value.SimpleStringValue;
                            break;
                        case "Processor":
                            obj.CpuModelName = information.Value.SimpleStringValue;
                            break;
                        case "CPU usage":
                            obj.CpuUsage = information.Value.ValueClass.Value;
                            break;
                        case "MemTotal":
                            obj.MemTotal = long.Parse(information.Value.SimpleStringValue);
                            break;
                        case "Memory usage":
                            obj.MemUsed = long.Parse(information.Value.ValueClass.Value.ToString());
                            break;
                        case "Kernel":
                            obj.Kernel = information.Value.SimpleStringValue;
                            break;
                        case "Uptime":
                            obj.Uptime = information.Value.SimpleStringValue;
                            break;
                        case "Load average":
                            obj.LoadAverage = information.Value.SimpleStringValue;
                            break;
                        case "configDirty":
                            obj.ConfigDirty = bool.Parse(information.Value.SimpleStringValue);
                            break;
                        case "rebootRequired":
                            obj.RebootRequired = bool.Parse(information.Value.SimpleStringValue);
                            break;
                        case "pkgUpdatesAvailable":
                            obj.PkgUpdatesAvailable = bool.Parse(information.Value.SimpleStringValue);
                            break;
                    }

                return obj;
            }

            return await RunCmd<OMVInformations>(new SystemInformationRequest());
        }

        public async Task<bool> IsRunning(string filename)
        {
            var result = await RunCmd<IsRunning>(new IsRunningRequest(filename)).ConfigureAwait(false);
            return result.Running;
        }


        public async Task<IEnumerable<PluginInfo>> GetPlugins()
        {
            var result = await RunCmd<IEnumerable<OmvPlugins>>(new EnumeratePluginsRequest());
            return _mapper.Map<IEnumerable<OmvPlugins>, IEnumerable<PluginInfo>>(result);
        }

        public Task<bool> Connect(string username, string password)
        {
           var b = Connect();
           return Task.FromResult(b);
        }

        public Task<string> GenerateRdd()
        {
            return RunCmd<string>(new GenerateRddRequest());
        }

        public Task<IEnumerable<byte[]>> GetRrdFiles(IEnumerable<string> filesPaths)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetRrdFile(string filePath)
        {
            throw new NotImplementedException();
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
            return result.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public Task<PowerManagementSetting> GetPowerManagementSetting()
        {
            return RunCmd<PowerManagementSetting>(new GetPowerManagementSettingRequest());
        }

        public Task<NetworkSetting> GetNetworkSetting()
        {
            return RunCmd<NetworkSetting>(new GetNetworkSettingRequest());
        }

        public Task<WebGuiSetting> GetWebGuiSettings()
        {
            return RunCmd<WebGuiSetting>(new GetWebGuiSettingRequest());
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

        public async Task<bool> SetWebGuiSettings(WebGuiSetting webGuiSetting)
        {
            var obj = await RunCmd<object>(new SetWebGuiSetting(webGuiSetting));
            return obj != null;
        }

        public async Task<T> RunCmd<T>(ISshRequest request)
        {
            var isRunningPidResult = await ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            LogError(isRunningPidResult);
            return isRunningPidResult.Data;
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

            result.Content = result.Content.Replace(@"\\\", string.Empty);
            result.Content = result.Content.Replace(@"\""", "\"");
            var toto = new Output<T> {Filename = result.Filename, Pos = result.Pos, Running = result.Running};

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
            var toto = new Output<string> {Filename = result.Filename, Pos = result.Pos, Running = result.Running};

            //var serializer = new OmvSerializer();
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

        public Task<IEnumerable<LogLine>> GetLogFile(OmvLogFileEnum logfile)
        {
            throw new NotImplementedException();
        }

      

        public Task<Output<T>> GetOutput<T>(string filename, int pos)
        {
            return RunCmd<Output<T>>(new GetOutputRequest(filename, pos));
        }


        public void SetOMVVersion(OMVVersion version)
        {
            _OMVVersions = version;
        }

        public OMVVersion GetRPCVersion()
        {
            if (_OMVVersions == null) CheckRpcVersion();

            return _OMVVersions;
        }


        private async Task CheckRpcVersion()
        {
            var result = await RunCmd<object>(new SystemInformationRequest());
            _OMVVersions = OMVVersions.GetVersionFromString(result.ToString());
        }

        /// <summary>
        ///     For OMV V4 else see <see cref="GetSystemInformations" />
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SystemInformation>> GetSystemInformation()
        {
            return RunCmd<IEnumerable<SystemInformation>>(new SystemInformationRequest());
        }


        private void LogError(ISshApiResponse apiResponse)
        {
            if (apiResponse.ResponseStatus == ResponseStatus.Error)
            {
                if (apiResponse.ErrorException != null)
                {
                    Logger.Error(apiResponse.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + apiResponse.Content + Environment.NewLine +
                                 apiResponse.ErrorException.Message);
                    return;
                }


                var omvError = apiResponse.Request.Handler.DeserializeError<ResponseError<OMVError>>(apiResponse);
                if (omvError != null)
                    Logger.Error(apiResponse.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + omvError.Error.Message);
                else if (apiResponse.ErrorMessage != null)
                    Logger.Error(apiResponse.Request.CommandText + Environment.NewLine +
                                 Environment.NewLine + apiResponse.ErrorMessage);
            }
        }

        public Task<IEnumerable<SharedFolder>> GetSharedFolders()
        {
            throw new NotImplementedException();
        }
    }
}