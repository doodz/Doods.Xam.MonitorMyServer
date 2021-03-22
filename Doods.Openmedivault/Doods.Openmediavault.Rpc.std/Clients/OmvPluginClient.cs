using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvPluginClient : OmvServiceClient
    {
        public OmvPluginClient(IRpcClient client) : base(client)
        {
            ServiceName = "Plugin";
        }

        public Task<IEnumerable<OmvPlugins>> GetPlugins()
        {
            var request = NewRequest("enumeratePlugins");
            var result = RunCmd<IEnumerable<OmvPlugins>>(request);

            return result;
        }

        public Task<string> InstallPlugins(IEnumerable<string> lst)
        {
            var request = NewRequest("install");
            request.Params = new {packages = lst};
            var result = RunCmd<string>(request);

            return result;
        }

        public Task<string> RemovePlugins(IEnumerable<string> lst)
        {
            var request = NewRequest("remove");
            request.Params = new {packages = lst};
            var result = RunCmd<string>(request);

            return result;
        }
    }
}