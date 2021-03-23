using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvPowerMgmtClient : OmvServiceClient
    {
        public OmvPowerMgmtClient(IRpcClient client) : base(client)
        {
            ServiceName = "PowerMgmt";
        }

        public Task<PowerManagementSetting> GetSettings()
        {
            var request = NewRequest("get");

            var result = RunCmd<PowerManagementSetting>(request);
            return result;
        }

        public Task<object> SetSettings(PowerManagementSetting settings)
        {
            var request = NewRequest("set");
            request.Params = settings;

            var result = RunCmd<object>(request);
            return result;
        }
    }
}