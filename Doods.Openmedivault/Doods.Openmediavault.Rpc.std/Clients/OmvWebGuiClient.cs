using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvWebGuiClient : OmvServiceClient
    {
        public OmvWebGuiClient(IRpcClient client) : base(client)
        {
            ServiceName = "WebGui";
        }

        public Task<WebGuiSetting> GetSettings()
        {
            var request = NewRequest("getSettings");

            var result = RunCmd<WebGuiSetting>(request);
            return result;
        }

        public Task<object> SetSettings(WebGuiSetting settings)
        {
            var request = NewRequest("setSettings");
            request.Params = settings;

            var result = RunCmd<object>(request);
            return result;
        }
    }
}