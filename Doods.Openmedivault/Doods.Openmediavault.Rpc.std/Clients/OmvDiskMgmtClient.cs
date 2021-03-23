using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvDiskMgmtClient : OmvServiceClient
    {
        public OmvDiskMgmtClient(IRpcClient client) : base(client)
        {
            ServiceName = "DiskMgmt";
        }

        public Task<string> GetDisksBackground()
        {
            var request = NewRequest("getListBg");
            var result = RunCmd<string>(request);
            return result;
        }
    }
}