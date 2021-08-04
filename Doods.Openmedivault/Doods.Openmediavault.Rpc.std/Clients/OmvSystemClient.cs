using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.Std.Data.V5;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvSystemClient : OmvServiceClient
    {
        public OmvSystemClient(IRpcClient client) : base(client)
        {
            ServiceName = "System";
        }

        public Task<IEnumerable<string>> GetTimeZoneList()
        {
            var request = NewRequest("getTimeZoneList");

            var result = RunCmd<IEnumerable<string>>(request);
            return result;
        }

        public Task<TimeSetting> GetDateAndTimeSetting()
        {
            var request = NewRequest("getTimeSettings");

            var result = RunCmd<TimeSetting>(request);
            return result;
        }


        public Task<object> SetDateAndTimeSetting(TimeSetting settings)
        {
            var request = NewRequest("setTimeSettings");
            request.Params = settings;

            var result = RunCmd<object>(request);
            return result;
        }


        public async Task<OMVInformations> GetSystemInformations()
        {
            await CheckRpcVersionAsync();
            var request = NewRequest("getInformation");
            request.Options = new Options {Updatelastaccess = false};


            if (GetRpcVersion() < OMVVersions.Version5)
            {
                var lst = await RunCmd<IEnumerable<SystemInformation>>(request);
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

            if (GetRpcVersion() == OMVVersions.Version5)
                return await RunCmd<OMVInformations>(request);

            var v6info= await RunCmd<Doods.Openmediavault.Rpc.Std.Data.V6.OMVInformations>(request);

            return v6info.ToV5();

        }
    }
}