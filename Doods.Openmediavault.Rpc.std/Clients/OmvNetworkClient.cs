using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvNetworkClient : OmvServiceClient
    {
        public Task<IEnumerable<Devices>> GetDevices()
        {
            var request = NewRequest("enumerateFilesystems");
            request.Params = new ParamsListFilter();

            var result = RunCmd<IEnumerable<Devices>>(request);

            return result;
        }
        public OmvNetworkClient(IRpcClient client) : base(client)
        {
            ServiceName = "Network";
        }

        public Task<NetworkSetting> GetSettings()
        {

            var request = NewRequest("getGeneralSettings");

            var result = RunCmd<NetworkSetting>(request);
            return result;
        }
        public Task<object> SetSettings(NetworkSetting settings)
        {

            var request = NewRequest("setGeneralSettings");
            request.Params = settings;

            var result = RunCmd<object>(request);
            return result;
        }
    }
}