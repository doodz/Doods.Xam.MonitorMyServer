using System.Collections;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvConfigClient : OmvServiceClient
    {



        public OmvConfigClient(IRpcClient client) : base(client)
        {
            ServiceName = "Config";
        }

        public Task<string> ApplyChangesBg()
        {

            var request = NewRequest("applyChangesBg");
            request.Params = new {modules = new ArrayList(),force=false};

            var result = RunCmd<string>(request);
            return result;
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